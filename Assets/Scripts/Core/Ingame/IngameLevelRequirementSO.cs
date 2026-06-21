using Common;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(menuName = Utility.DefaultScriptableObjectPrefix + "Ingame Level Requirement")]
    public class IngameLevelRequirementSO : ContainedSO
    {
        [field: SerializeField] public float[] PresetValues { get; private set; }

        public float GetRequiredExperienceForLevel(int level)
        {
            if (level < PresetValues.Length)
            {
                return PresetValues[level];
            }
            return PresetValues[^1] * Mathf.Exp(1 + level - PresetValues.Length);
        }
    }
}
