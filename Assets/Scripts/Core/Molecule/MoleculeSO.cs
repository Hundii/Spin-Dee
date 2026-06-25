using Common;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(menuName = Utility.DefaultScriptableObjectPrefix + "Molecule/Definition")]
    public class MoleculeSO : ContainedSO
    {
        public Molecule prefab;
        public Color[] moleculeColors;
        public float amount;
    }
}
