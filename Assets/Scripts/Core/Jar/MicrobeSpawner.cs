using Common;
using Core.Generated;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core
{
    public class MicrobeSpawner : GenericSpawner, INonPersistentManager
    {
        [Header("References")]
        [SerializeField] private Transform spawnedObjectsParent;
        [SerializeField] private Jar jar;

        private List<LiquidSpawnData> liquidSpawnData;


        private StatSOContainer statSOContainer;
        private StatsHandler statsHandler;
        private RoundAmplifierHandler roundAmplifierHandler;
        private RoundHandler roundHandler;

        private List<GameObject> spawnedMicrobes = new();

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
            roundHandler = this.Inject<RoundHandler>();
            roundAmplifierHandler = this.Inject<RoundAmplifierHandler>();
            var liquidSO = jar.GetLiquidSO();
            liquidSpawnData = jar.GetLiquidSO().spawnData;

            statsHandler = new(new(
                    new List<Stat>()
                    {
                        SOContainerContainer.StatSOContainer.microbeSpawnRate 
                    }, 
                    new List<double>() 
                    { 
                        liquidSO.microbeSpawnChancePerSecond
                    }));
            statsHandler.valueChanged += HandleStatChanged;
            HandleStatChanged();

            SpawnMicrobe();
            StartCoroutine(TickSpawn());
        }

        public void SpawnMicrobe()
        {
            var randomPoint = GetRandomPointInSpawnArea();
            var spawnDataIndex = RandomUtility.RandomWeightedTable(liquidSpawnData.Select(x => x.weight).ToArray());
            var microbePrefab = liquidSpawnData[spawnDataIndex].microbe.prefab;
            var microbe = Instantiate(microbePrefab, randomPoint,Quaternion.identity);
            microbe.transform.SetParent(spawnedObjectsParent);
            spawnedMicrobes.Add(microbe);
        }

        public void SpawnBossMicrobe()
        {
            var bossSpawnData = jar.GetLiquidSO().bossSpawnData;
            var randomPoint = GetRandomPointInSpawnArea();
            var spawnDataIndex = RandomUtility.RandomWeightedTable(bossSpawnData.Select(x => x.weight).ToArray());
            var microbePrefab = bossSpawnData[spawnDataIndex].microbe.prefab;
            var microbe = Instantiate(microbePrefab, randomPoint, Quaternion.identity);
            microbe.transform.SetParent(spawnedObjectsParent);
            spawnedMicrobes.Add(microbe);
        }

        public void SpawnMicrobe(Vector3 position, GameObject microbe)
        {
            var spawnedMicrobe = Instantiate(microbe, position, Quaternion.identity);
            spawnedMicrobe.transform.SetParent(spawnedObjectsParent);
        }

        private void HandleStatChanged()
        {
            statsHandler.TryGetAttributeValue(statSOContainer.microbeSpawnRate, out var value);
            spawnChancePerSecond = (float)value;
        }

        private void HandleRoundEnded()
        {
            foreach (var microbe in spawnedMicrobes)
            {
                if (microbe != null)
                {
                    Destroy(microbe);
                }
            }
            spawnedMicrobes = new();
        }

        private void HandleRoundStarted()
        {
            statsHandler.RegisterAmplifiers(roundAmplifierHandler.CurrentMicrobeSpawnRate);
            if (roundHandler.IsCurrentRoundBossRound)
            {
                SpawnBossMicrobe();
            }
            else
            {
                SpawnMicrobe();
            }
        }

        IEnumerator TickSpawn()
        {
            var wait = new WaitForSeconds(1 / spawnTriesPerSecond);
            while (true)
            {
                float bossDivider = roundHandler.IsCurrentRoundBossRound ? 2 : 1;
                if (Random.Range(0f, 1f) <= (spawnChancePerSecond / bossDivider / (100 * spawnTriesPerSecond)))
                {
                    SpawnMicrobe();
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
