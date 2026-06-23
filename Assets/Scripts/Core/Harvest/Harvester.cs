using Common;
using Core.Generated;
using UnityEngine;

namespace Core
{
    public class Harvester : MonoBehaviour
    {
        private HarvesterStatsSO harvesterStats;

        private float harvestCooldown;
        private float cooldownMultiplierInside;
        private float cooldownBonusAfterLeavingFlatAmount;
        private float cooldownBonusAfterLeavingPercentage;
        private float harvestAmount;

        private float currentCooldown;

        private StatsHandler statsHandler;
        private StatSOContainer statSOContainer;
        public GameEvent<float> MaterialHarvested { get; private set; } = new();

        private void Start()
        {
            harvesterStats = GetComponentInParent<IHarvesterStatsHolder>().GetHarvesterStats();
            currentCooldown = harvestCooldown;
            statSOContainer = SOContainerContainer.StatSOContainer;
            statsHandler = GetComponentInParent<IStatsHandlerHolder>().GetStatsHandler();
            statsHandler.valueChanged += HandleStatChange;
            HandleStatChange();
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.TryGetComponent(out IHarvestable harvestable))
            {
                if (ModifyCooldown(Time.deltaTime))
                {
                    if (harvestable.Harvest(harvestAmount,out var value))
                    {
                        HarvestAmount(value);
                    }
                }
            }
        }

        private void OnTriggerStay(Collider collider)
        {
            if (collider.gameObject.TryGetComponent(out IHarvestable harvestable))
            {
                if(ModifyCooldown(Time.deltaTime * cooldownMultiplierInside))
                {
                    if (harvestable.Harvest(harvestAmount, out var value))
                    {
                        HarvestAmount(value);
                    }
                }
            }
        }

        private void OnTriggerExit(Collider collider)
        {
            if (collider.gameObject.TryGetComponent(out IHarvestable harvestable))
            {
                if (ModifyCooldown(cooldownBonusAfterLeavingFlatAmount +  harvestCooldown * (cooldownBonusAfterLeavingPercentage / 100)))
                {
                    if (harvestable.Harvest(harvestAmount, out var value))
                    {
                        HarvestAmount(value);
                    }
                }
            }
        }

        public bool ModifyCooldown(float amount)
        {
            currentCooldown -= amount;
            if (currentCooldown <= 0f)
            {
                currentCooldown = harvestCooldown;
                return true;
            }
            return false;
        }

        private void HarvestAmount(float amount)
        {
            MaterialHarvested.Invoke(amount);
        }

        private void HandleStatChange()
        {
            statsHandler.TryGetAttributeValue(statSOContainer.harvestSpeed, out var harvestSpeed);
            var harvestSpeedF = (float)harvestSpeed;
            harvestCooldown = harvesterStats.baseHarvestCooldown / harvestSpeedF;
            cooldownMultiplierInside = harvesterStats.baseCooldownMultiplierInside * harvestSpeedF;
            cooldownBonusAfterLeavingFlatAmount = harvesterStats.baseCooldownBonusAfterLeavingFlatAmount * harvestSpeedF;
            cooldownBonusAfterLeavingPercentage = harvesterStats.baseCooldownBonusAfterLeavingPercentage * harvestSpeedF;

            statsHandler.TryGetAttributeValue(statSOContainer.harvestAmount, out var harvestAmount);
            this.harvestAmount = harvesterStats.baseHarvestAmount * (float)harvestAmount;
        }
    }
}
