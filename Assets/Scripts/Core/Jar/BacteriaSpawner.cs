using UnityEngine;

namespace Core
{
    public class BacteriaSpawner : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Collider spawnArea;
        [SerializeField] private Transform spawnedObjectsParent;
        [SerializeField] private GameObject bacteriaPrefab;

        void Start()
        {
            SpawnBacteria();
        }

        public void SpawnBacteria()
        {
            var randomPoint = GetRandomPointInSpawnArea();
            var bacteria = Instantiate(bacteriaPrefab,randomPoint,Quaternion.identity);
            bacteria.transform.SetParent(spawnedObjectsParent);
        }

        private Vector3 GetRandomPointInSpawnArea()
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
