using Common;
using System.Collections.Generic;
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

        private List<ScoreBoosterSO> scoreBoosters = new();

        private void OnEnable()
        {
            IngameEvents.MicrobeKilledByPlayer += HandleMicrobeKilled;
            IngameEvents.MoleculeMaterialHarvestedByMicrobe += HandleMaterialHarvestedByMicrobe;
            IngameEvents.MoleculeMaterialHarvestedByPlayer += HandleMaterialHarvestedByPlayer;
            IngameEvents.ScoreBoosterGained.RegisterListener(HandleScoreBoosterActivated);
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
            return 100f;
        }

        public void HandleScoreBoosterActivated(ScoreBoosterSO scoreBoosterSO)
        {
            scoreBoosters.Add(scoreBoosterSO);
            Debug.Log(CurrentScore);
            CurrentScore += scoreBoosterSO.scoreAmount;
            Debug.Log(scoreBoosterSO.scoreAmount);
            Debug.Log(CurrentScore);
        }

        private void OnDisable()
        {
            IngameEvents.MicrobeKilledByPlayer -= HandleMicrobeKilled;
            IngameEvents.MoleculeMaterialHarvestedByMicrobe -= HandleMaterialHarvestedByMicrobe;
            IngameEvents.MoleculeMaterialHarvestedByPlayer -= HandleMaterialHarvestedByPlayer;
            IngameEvents.ScoreBoosterGained -= HandleScoreBoosterActivated;
        }
    }
}
