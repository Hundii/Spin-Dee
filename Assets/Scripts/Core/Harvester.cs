using UnityEngine;

namespace Core
{
    public class Harvester : MonoBehaviour
    {
        [SerializeField] private float harvestCooldown = 1f;
        [SerializeField] private float cooldownMultiplierInside = 1.25f;
        [SerializeField] private float cooldownBonusAfterLeavingPercentage = 15f;

        public float currentCooldown;

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
                    HarvestAmount(harvestable.Harvest());
                }
            }
        }

        private void OnTriggerStay(Collider collider)
        {
            if (collider.gameObject.TryGetComponent(out IHarvestable harvestable))
            {
                if(ModifyCooldown(Time.deltaTime * cooldownMultiplierInside))
                {
                    HarvestAmount(harvestable.Harvest());
                }
            }
        }

        private void OnTriggerExit(Collider collider)
        {
            if (collider.gameObject.TryGetComponent(out IHarvestable harvestable))
            {
                if (ModifyCooldown(harvestCooldown * (cooldownBonusAfterLeavingPercentage / 100)))
                {
                    HarvestAmount(harvestable.Harvest());
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
            Debug.Log($"Harvested {amount}");
        }
    }
}
