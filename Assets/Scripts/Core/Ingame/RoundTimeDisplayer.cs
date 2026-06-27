using Common;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Core
{
    public class RoundTimeDisplayer : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TextMeshProUGUI roundTimeText;
        [SerializeField] private TextMeshProUGUI roundAmountText;

        [Header("Score Display")]
        [SerializeField] private Color scoreIsEnoughColor;
        [SerializeField] private Color scoreIsNotEnoughColor;
        [SerializeField] private int refreshRatePerSecond = 10;

        private RoundHandler roundHandler;
        private ScoreCalculator scoreCalculator;

        private void OnEnable()
        {
            IngameEvents.RoundEnded.RegisterListener(UpdateRoundText);
            IngameEvents.RoundStarted.RegisterListener(UpdateRoundText);
        }

        private void Start()
        {
            roundHandler = this.Inject<RoundHandler>();
            scoreCalculator = this.Inject<ScoreCalculator>();
            StartCoroutine(UpdateTimeDisplay());

            UpdateRoundText();
        }

        IEnumerator UpdateTimeDisplay()
        {
            var wait = new WaitForSeconds(1f / refreshRatePerSecond);
            while (true)
            {
                roundTimeText.text = roundHandler.GetCurrentRoundRemainingTime().AsRoundStr(1);
                if (roundHandler.GetCurrentRoundRemainingTime() <= 15)
                {
                    if (scoreCalculator.CurrentScore >= scoreCalculator.GetScoreRequirement())
                    {
                        roundTimeText.color = scoreIsEnoughColor;
                    }
                    else
                    {
                        roundTimeText.color = scoreIsNotEnoughColor;
                    }
                }
                else
                {
                    roundTimeText.color = Color.white;
                }
                
                yield return wait;
            }
        }

        public void UpdateRoundText()
        {
            var currentRound = roundHandler.CurrentRound;
            var maxRounds = roundHandler.GetMaxRound();
            roundAmountText.text = $"{currentRound} / {maxRounds}";
        }

        private void OnDisable()
        {
            IngameEvents.RoundEnded.UnRegisterListener(UpdateRoundText);
            IngameEvents.RoundStarted.UnRegisterListener(UpdateRoundText);
        }

    }
}
