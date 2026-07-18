using Common;
using TMPro;
using UnityEngine;

namespace Core
{
    public class ScoreDisplayer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;

        private ScoreCalculator scoreCalculator;

        private SubscriptionList subscriptions = new();
        private void OnEnable()
        {
            subscriptions.Add(IngameEvents.ScoreChanged.RegisterListener(new(UpdateScoreDisplay)));
        }

        private void Start()
        {
            scoreCalculator = this.Inject<ScoreCalculator>();
            UpdateScoreDisplay();
        }

        public void UpdateScoreDisplay()
        {
            scoreText.text = $"{scoreCalculator.CurrentScore.AsRoundStr(1)} / {scoreCalculator.GetScoreRequirement().AsRoundStr(1)}";
        }

        private void OnDisable()
        {
            subscriptions.UnsubscribeAll();
        }
    }
}
