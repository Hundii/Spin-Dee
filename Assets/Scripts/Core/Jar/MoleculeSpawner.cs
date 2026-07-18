using Common;
using Core.Generated;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class MoleculeSpawner : GenericSpawner, INonPersistentManager
    {
        [SerializeField] private Transform spawnedObjectsParent;
        [SerializeField] private MoleculeSO moleculeSO;
        [SerializeField] private Jar jar;


        private StatSOContainer statSOContainer;
        private StatsHandler statsHandler;
        private RoundAmplifierHandler roundAmplifierHandler;

        private List<GameObject> spawnedMolecules = new();

        private float spawnChancePerSecond;

        private SubscriptionList subscriptionList = new();
        private void OnEnable()
        {
            subscriptionList.Add(IngameEvents.RoundEnded.RegisterListener(new(HandleRoundEnded)));
            subscriptionList.Add(IngameEvents.RoundStarted.RegisterListener(new(HandleRoundStarted)));
        }
        protected override void Start()
        {
            base.Start();
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
            spawnedMolecules.Add(molecule.gameObject);
            molecule.transform.SetParent(spawnedObjectsParent);
            molecule.Init(moleculeSO);
        }

        private void HandleStatChanged()
        {
            statsHandler.TryGetAttributeValue(statSOContainer.moleculeSpawnRate, out var value);
            spawnChancePerSecond = (float)value;
        }
        private void HandleRoundEnded()
        {
            foreach (var molecule in spawnedMolecules)
            {
                if (molecule != null)
                {
                    Destroy(molecule);
                }
            }
            spawnedMolecules = new();
        }
        private void HandleRoundStarted()
        {
            statsHandler.RegisterAmplifiers(roundAmplifierHandler.CurrentMoleculeSpawnRate);
            SpawnMolecule();
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
            subscriptionList.UnsubscribeAll();
        }
    }
}
