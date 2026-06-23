using Common;
using System.Collections;
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

        private float spawnChancePerSecond;

        private void Awake()
        {
            jar = GetComponent<Jar>();
            var liquidSO = jar.GetLiquidSO();
            liquidSpawnData = jar.GetLiquidSO().spawnData;
            spawnChancePerSecond = liquidSO.microbeSpawnChancePerSecond;
        }

        void Start()
        {
            SpawnBacteria();
            SpawnBacteria();
            SpawnBacteria();
            SpawnBacteria();
            SpawnBacteria();
            SpawnBacteria();
            StartCoroutine(TickSpawn());
        }

        public void SpawnBacteria()
        {
            var randomPoint = GetRandomPointInSpawnArea();
            var spawnDataIndex = RandomUtility.RandomWeightedTable(liquidSpawnData.Select(x => x.weight).ToArray());
            var bacteriaPrefab = liquidSpawnData[spawnDataIndex].microbe.prefab;
            var bacteria = Instantiate(bacteriaPrefab,randomPoint,Quaternion.identity);
            bacteria.transform.SetParent(spawnedObjectsParent);
        }

        IEnumerator TickSpawn()
        {
            var wait = new WaitForSeconds(1 / spawnTriesPerSecond);
            while (true)
            {
                if (Random.Range(0f, 1f) <= (spawnChancePerSecond / (100 * spawnTriesPerSecond)))
                {
                    SpawnBacteria();
                }
                yield return wait;
            }
        }
    }
}
