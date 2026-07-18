using Common;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core
{
    public class Capsule : MonoBehaviour, IStatsHandlerHolder, IHarvesterStatsHolder, INonPersistentManager
    {
        [SerializeField] private MeshRenderer meshRenderer;
        private CapsuleSO capsuleStatsSO;
        private List<Harvester> harvesters = new();
        public StatsHandler StatsHandler { get; private set; }

        private List<StatBoosterSO> boosters = new();

        private SubscriptionList subscriptions = new();
        private void OnEnable()
        {
            subscriptions.Add(IngameEvents.CapsuleBoosterGained.RegisterListener(new(RegisterBooster)));
        }

        private void Awake()
        {
            var capsuleSO = this.Inject<SharedDataService>().GetData<GameSelectionSceneData>(false)?.capsuleStatSO;
            capsuleStatsSO = capsuleSO == null ? SOContainerContainer.CapsuleSOContainer.glitch : capsuleSO;
            meshRenderer.material = capsuleStatsSO.capsuleMaterial;

            StatsHandler = new(new(capsuleStatsSO.stats.Select(x=>x.stat),capsuleStatsSO.stats.Select(x=>x.value)));

            this.Inject<PlayerManager>().RegisterPlayer(this);
        }

        private void Start()
        {
            harvesters = GetComponentsInChildren<Harvester>().ToList();

            foreach (var harvester in harvesters)
            {
                harvester.MaterialHarvested.RegisterListener(new(HandleHarvestedMaterial));
            }

        }

        private void HandleHarvestedMaterial(float amount)
        {
            IngameEvents.MoleculeMaterialHarvestedByPlayer.Invoke(amount);
        }

        public StatsHandler GetStatsHandler()
        {
            return StatsHandler;
        }

        public HarvesterStatsSO GetHarvesterStats()
        {
            return capsuleStatsSO.harvesterStatsSO;
        }

        public void RegisterBooster(StatBoosterSO booster)
        {
            boosters.Add(booster);
            StatsHandler.RegisterAmplifiers(booster.amplifier);
            StatsHandler.TryGetAttributeValue(booster.amplifier.stat, out var value);
        }

        private void OnDisable()
        {
            subscriptions.UnsubscribeAll();
        }
    }
}
