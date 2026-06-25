using Common;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(menuName = Utility.DefaultScriptableObjectPrefix + "Jar/Definition")]
    public class JarSO : ContainedSO
    {
        public Sprite icon;
        public Material jarMaterial;
        public int roundsBeforeBoss;
        public int maxRounds;
        public string description;
        public bool isStarter;
    }
}
