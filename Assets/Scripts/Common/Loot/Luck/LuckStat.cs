using UnityEngine;

namespace Common
{
    [CreateAssetMenu(menuName = "Common/Stat/Luck")]
    public class LuckStat : Stat
    {
        public LuckStrategy luckStrategy;
    }
}
