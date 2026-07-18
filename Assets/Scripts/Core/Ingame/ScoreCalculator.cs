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

        private SubscriptionList subscriptionList = new();

        private void OnEnable()
        {
            subscriptionList.Add(IngameEvents.MicrobeKilledByPlayer.RegisterListener(new(HandleMicrobeKilled)));
            subscriptionList.Add(IngameEvents.MoleculeMaterialHarvestedByMicrobe.RegisterListener(new(HandleMaterialHarvestedByMicrobe)));
            subscriptionList.Add(IngameEvents.MoleculeMaterialHarvestedByPlayer.RegisterListener(new(HandleMaterialHarvestedByPlayer)));
            subscriptionList.Add(IngameEvents.ScoreBoosterGained.RegisterListener(new(HandleScoreBoosterActivated)));
            subscriptionList.Add(IngameEvents.RoundEnded.RegisterListener(new(HandleRoundEnded)));
        }

        private void Start()
        {
            roundHandler = this.Inject<RoundHandler>();
        }

        private void HandleMicrobeKilled(Microbe microbe)
        {
            CurrentScore += microbe.GetExperiencePointsWorth();
        }

        private void HandleMaterialHarvestedByPlayer(float amount)
        {
            CurrentScore += amount;
        }

        private void HandleMaterialHarvestedByMicrobe(float amount)
        {
            CurrentScore -= 2f * amount;
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
            subscriptionList.UnsubscribeAll();
        }
    }
}
