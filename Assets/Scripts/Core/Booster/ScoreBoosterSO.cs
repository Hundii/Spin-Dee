using UnityEngine;

namespace Core
{
    [CreateAssetMenu(menuName = Utility.DefaultScriptableObjectPrefix + "Booster/Score")]
    public class ScoreBoosterSO : BoosterSO
    {
        public float scoreAmount;

        public override string GetDisplayString()
        {
            return $"+ {scoreAmount} score instantly";
        }

        public override void Activate()
        {
            IngameEvents.ScoreBoosterGained.Invoke(this);
        }
    }
}
