using Common;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(menuName = Utility.DefaultScriptableObjectPrefix + "Harvester Stats")]
    public class HarvesterStatsSO : ContainedSO
    {
        public float baseHarvestCooldown = 1f;
        public float baseCooldownMultiplierInside = 0.5f;
        public float baseCooldownBonusAfterLeavingPercentage = 15f;
        public float baseCooldownBonusAfterLeavingFlatAmount = 0.1f;
        public float baseHarvestAmount = 1f;
    }
}
