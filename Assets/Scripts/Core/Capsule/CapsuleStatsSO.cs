using Common;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(menuName = Utility.DefaultScriptableObjectPrefix + "Capsule/Capsule Stat")]
    public class CapsuleStatsSO : ContainedSO
    {
        public List<StatSelector> stats;
        public HarvesterStatsSO harvesterStatsSO;
    }
}
