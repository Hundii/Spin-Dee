using Common;
using System.Collections;
using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(Jar))]
    public class MoleculeSpawner : GenericSpawner, INonPersistentManager
    {
        [SerializeField] private Transform spawnedObjectsParent;
        [SerializeField] private MoleculeSO moleculeSO;

        private Jar jar;

        private float spawnChancePerSecond;

        void Start()
        {
            jar = GetComponent<Jar>();
            spawnChancePerSecond = jar.GetLiquidSO().moleculeSpawnChancePerSecond;

            SpawnMolecule();
            SpawnMolecule();
            StartCoroutine(TickSpawn());
        }

        public void SpawnMolecule()
        {
            var randomPoint = GetRandomPointInSpawnArea();
            var molecule = Instantiate(moleculeSO.prefab, randomPoint, Quaternion.identity);
            molecule.transform.SetParent(spawnedObjectsParent);
            molecule.Init(moleculeSO);
        }

        IEnumerator TickSpawn()
        {
            var wait = new WaitForSeconds(1 / spawnTriesPerSecond);
            while(true)
            {
                if (Random.Range(0f,1f) <= (spawnChancePerSecond / (100f * spawnTriesPerSecond)))
                {
                    SpawnMolecule();
                }
                yield return wait;
            }
        }
    }
}
