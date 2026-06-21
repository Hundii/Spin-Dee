using UnityEngine;

namespace Common
{
    [CreateAssetMenu(menuName = "Common/Stat/Base")]
    public class Stat : ContainedSO
    {
        public string attributeName;
        public StatType statType;
        public double defaultValue;
    }
}
