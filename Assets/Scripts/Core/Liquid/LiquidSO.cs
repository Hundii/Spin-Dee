using Common;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(menuName = Utility.DefaultScriptableObjectPrefix + "Liquid/Definition")]
    public class LiquidSO : ContainedSO
    {
        public Material liquidMaterial;
        public List<LiquidSpawnData> spawnData;
        public float microbeSpawnRate;
        public float moleculeSpawnRate;
    }
}
