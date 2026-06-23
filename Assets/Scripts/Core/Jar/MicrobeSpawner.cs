using Common;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(Jar))]
    public class MicrobeSpawner : GenericSpawner
    {
        [Header("References")]
        [SerializeField] private Transform spawnedObjectsParent;

        private List<LiquidSpawnData> liquidSpawnData;

        private Jar jar;

        private void Awake()
        {
            jar = GetComponent<Jar>();
            liquidSpawnData = jar.GetLiquidSO().spawnData;
        }

        void Start()
        {
            SpawnBacteria();
            SpawnBacteria();
            SpawnBacteria();
            SpawnBacteria();
            SpawnBacteria();
            SpawnBacteria();
        }

        public void SpawnBacteria()
        {
            var randomPoint = GetRandomPointInSpawnArea();
            var spawnDataIndex = RandomUtility.RandomWeightedTable(liquidSpawnData.Select(x => x.weight).ToArray());
            var bacteriaPrefab = liquidSpawnData[spawnDataIndex].microbe.prefab;
            var bacteria = Instantiate(bacteriaPrefab,randomPoint,Quaternion.identity);
            bacteria.transform.SetParent(spawnedObjectsParent);
        }

        
    }
}
