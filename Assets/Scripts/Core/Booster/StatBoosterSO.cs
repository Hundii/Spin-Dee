using Common;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(menuName = Utility.DefaultScriptableObjectPrefix + "Booster/Stat")]
    public class StatBoosterSO : BoosterSO
    {
        public Amplifier amplifier;

        public override string GetDisplayString()
        {
            return amplifier.GetDisplayString();
        }

        public override void Activate()
        {
            IngameEvents.CapsuleBoosterGained.Invoke(this);
        }
    }
}
