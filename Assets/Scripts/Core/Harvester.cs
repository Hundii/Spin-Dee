using Common;
using UnityEngine;

namespace Core
{
    public class Harvester : MonoBehaviour
    {
        [SerializeField] private float harvestCooldown = 1f;
        [SerializeField] private float cooldownMultiplierInside = 0.5f;
        [SerializeField] private float cooldownBonusAfterLeavingPercentage = 15f;
        [SerializeField] private float cooldownBonusAfterLeavingFlatAmount = 0.1f;

        private float currentCooldown;

        public GameEvent<float> MaterialHarvested { get; private set; } = new();

        private void Start()
        {
            currentCooldown = harvestCooldown;
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.TryGetComponent(out IHarvestable harvestable))
            {
                if (ModifyCooldown(Time.deltaTime))
                {
                    if (harvestable.Harvest(out var value))
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
                    if (harvestable.Harvest(out var value))
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
                    if (harvestable.Harvest(out var value))
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
    }
}
