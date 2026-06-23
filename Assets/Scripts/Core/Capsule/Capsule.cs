using Common;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core
{
    public class Capsule : MonoBehaviour, IStatsHandlerHolder, IHarvesterStatsHolder
    {
        private CapsuleStatsSO capsuleStatsSO;
        private List<Harvester> harvesters = new();
        public StatsHandler StatsHandler { get; private set; }

        private void Awake()
        {
            capsuleStatsSO = SOContainerContainer.CapsuleStatsSOContainer.god;
            StatsHandler = new(new(capsuleStatsSO.stats.Select(x=>x.stat),capsuleStatsSO.stats.Select(x=>x.value)));
        }

        private void Start()
        {
            harvesters = GetComponentsInChildren<Harvester>().ToList();

            foreach (var harvester in harvesters)
            {
                harvester.MaterialHarvested.RegisterListener(HandleHarvestedMaterial);
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
    }
}
