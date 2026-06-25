using Common;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(menuName = Utility.DefaultScriptableObjectPrefix + "Capsule/Definition")]
    public class CapsuleSO : ContainedSO
    {
        public Sprite icon;
        public Material capsuleMaterial;
        public GameObject prefab;
        public List<StatSelector> stats;
        public HarvesterStatsSO harvesterStatsSO;
        public string decription;
        public bool isStarter;
    }
}
