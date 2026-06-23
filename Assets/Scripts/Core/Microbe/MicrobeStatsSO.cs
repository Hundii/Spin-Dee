using Common;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(menuName = Utility.DefaultScriptableObjectPrefix + "Microbe/Stats")]
    public class MicrobeStatsSO : ContainedSO
    {
        public List<StatSelector> stats;
        public HarvesterStatsSO harvesterStats;
    }
}
