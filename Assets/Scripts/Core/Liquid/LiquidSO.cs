using Common;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(menuName = Utility.DefaultScriptableObjectPrefix + "Liquid/Definition")]
    public class LiquidSO : ContainedSO
    {
        public Sprite icon;
        public Material liquidMaterial;
        public List<LiquidSpawnData> spawnData;
        public List<LiquidSpawnData> bossSpawnData;
        public float microbeSpawnChancePerSecond;
        public float moleculeSpawnChancePerSecond;
        public string description;
        public bool isStarter;
    }
}
