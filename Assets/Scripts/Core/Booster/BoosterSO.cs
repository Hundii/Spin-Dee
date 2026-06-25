using Common;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(menuName = Utility.DefaultScriptableObjectPrefix + "Booster/Base")]
    public class BoosterSO : ContainedSO
    {
        public Sprite icon;
        public GeneralRarity generalRarity;

        public virtual void Activate() { }
        public virtual string GetDisplayString() => "";
    }
}
