using Common;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Core
{
    public abstract class GenericSpawner : MonoBehaviour
    {
        [SerializeField] protected float spawnTriesPerSecond = 5f;

        [Header("References")]
        [SerializeField] protected Collider spawnArea;

        private Transform capsule;

        protected virtual void Start()
        {
            capsule = this.Inject<PlayerManager>().GetPlayer<Capsule>().transform;
        }

        protected Vector3 GetRandomPointInSpawnArea()
        {
            Collider box = spawnArea;

            Bounds bounds = box.bounds;

            Vector3 randomPoint;
            Vector3 capsuleCenter = capsule.position;
            float avoidRadiusSqr = 2f * 2f;

            do
            {
                randomPoint = new Vector3(
                    Random.Range(bounds.min.x, bounds.max.x),
                    Random.Range(bounds.min.y, bounds.max.y),
                    Random.Range(bounds.min.z, bounds.max.z)
                );

            } while ((new Vector2(randomPoint.x - capsuleCenter.x,
                                  randomPoint.z - capsuleCenter.z)).sqrMagnitude < avoidRadiusSqr);



            return randomPoint;
        }
    }
}
