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

        private RoundHandler roundHandler;

        private void OnEnable()
        {
            IngameEvents.MicrobeKilledByPlayer += HandleMicrobeKilled;
            IngameEvents.MoleculeMaterialHarvestedByMicrobe += HandleMaterialHarvestedByMicrobe;
            IngameEvents.MoleculeMaterialHarvestedByPlayer += HandleMaterialHarvestedByPlayer;
            IngameEvents.ScoreBoosterGained += HandleScoreBoosterActivated;
            IngameEvents.RoundEnded += HandleRoundEnded;
        }

        private void Start()
        {
            roundHandler = this.Inject<RoundHandler>();
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
            CurrentScore += scoreBoosterSO.scoreAmount;
        }

        private void HandleRoundEnded()
        {
            if (CurrentScore < GetScoreRequirement())
            {
                IngameEvents.RoundLost.Invoke();
                return;
            }
            CurrentScore = 0f;
            if (roundHandler.CurrentRound >= roundHandler.GetMaxRound())
            {
                IngameEvents.GameWon.Invoke();
            }
            else
            {
                IngameEvents.RoundWon.Invoke();
            }
        }

        private void OnDisable()
        {
            IngameEvents.MicrobeKilledByPlayer -= HandleMicrobeKilled;
            IngameEvents.MoleculeMaterialHarvestedByMicrobe -= HandleMaterialHarvestedByMicrobe;
            IngameEvents.MoleculeMaterialHarvestedByPlayer -= HandleMaterialHarvestedByPlayer;
            IngameEvents.ScoreBoosterGained -= HandleScoreBoosterActivated;
            IngameEvents.RoundEnded -= HandleRoundEnded;
        }
    }
}
