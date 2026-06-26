using Common;
using Core.Generated;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(Jar))]
    public class MoleculeSpawner : GenericSpawner, INonPersistentManager
    {
        [SerializeField] private Transform spawnedObjectsParent;
        [SerializeField] private MoleculeSO moleculeSO;

        private Jar jar;

        private StatSOContainer statSOContainer;
        private StatsHandler statsHandler;
        private RoundAmplifierHandler roundAmplifierHandler;

        private float spawnChancePerSecond;

        private void OnEnable()
        {
            IngameEvents.RoundStarted += HandleRoundStarted;
        }
        void Start()
        {
            jar = GetComponent<Jar>();
            statSOContainer = SOContainerContainer.StatSOContainer;
            roundAmplifierHandler = this.Inject<RoundAmplifierHandler>();

            statsHandler = new(new(
                    new List<Stat>()
                    {
                        SOContainerContainer.StatSOContainer.moleculeSpawnRate
                    },
                    new List<double>()
                    {
                        jar.GetLiquidSO().moleculeSpawnChancePerSecond
                    }));
            statsHandler.valueChanged += HandleStatChanged;
            HandleStatChanged();

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

        private void HandleStatChanged()
        {
            statsHandler.TryGetAttributeValue(statSOContainer.moleculeSpawnRate, out var value);
            spawnChancePerSecond = (float)value;
        }

        private void HandleRoundStarted()
        {
            statsHandler.RegisterAmplifiers(roundAmplifierHandler.CurrentMoleculeSpawnRate);
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

        private void OnDisable()
        {
            IngameEvents.RoundStarted -= HandleRoundStarted;
        }
    }
}
