using UnityEngine;

namespace Core
{
    public abstract class GenericSpawner : MonoBehaviour
    {
        [SerializeField] protected float spawnTriesPerSecond = 5f;

        [Header("References")]
        [SerializeField] protected Collider spawnArea;

        protected Vector3 GetRandomPointInSpawnArea()
        {
            Collider box = spawnArea;

            Bounds bounds = box.bounds;

            Vector3 randomPoint = new(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y),
                Random.Range(bounds.min.z, bounds.max.z)
            );

            return randomPoint;
        }
    }
}
