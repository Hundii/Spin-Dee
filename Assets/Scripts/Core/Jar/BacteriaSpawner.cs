using UnityEngine;

namespace Core
{
    public class BacteriaSpawner : GenericSpawner
    {
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

        
    }
}
