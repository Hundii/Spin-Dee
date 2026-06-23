using Common;
using UnityEngine;

namespace Core
{
    public class ScoreCalculator : MonoBehaviour, INonPersistentManager
    {
        private float _currentScore;
        public float CurrentScore
        {
            get
            {
                return _currentScore;
            }
            private set
            {
                _currentScore = value;
                IngameEvents.ScoreChanged.Invoke(value);
            }
        }

        private void OnEnable()
        {
            IngameEvents.MicrobeKilledByPlayer += HandleMicrobeKilled;
            IngameEvents.MoleculeMaterialHarvestedByMicrobe += HandleMaterialHarvestedByMicrobe;
            IngameEvents.MoleculeMaterialHarvestedByPlayer += HandleMaterialHarvestedByPlayer;
        }

        private void HandleMicrobeKilled(Microbe microbe)
        {
            CurrentScore += 10f;
        }

        private void HandleMaterialHarvestedByPlayer(float amount)
        {
            CurrentScore += 1f;
        }

        private void HandleMaterialHarvestedByMicrobe(float amount)
        {
            CurrentScore -= 2f;
        }

        public float GetScoreRequirement()
        {
            return 25f;
        }

        private void OnDisable()
        {
            IngameEvents.MicrobeKilledByPlayer -= HandleMicrobeKilled;
            IngameEvents.MoleculeMaterialHarvestedByMicrobe -= HandleMaterialHarvestedByMicrobe;
            IngameEvents.MoleculeMaterialHarvestedByPlayer -= HandleMaterialHarvestedByPlayer;
        }
    }
}
