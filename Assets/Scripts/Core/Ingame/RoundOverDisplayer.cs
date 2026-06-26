using Common;
using TMPro;
using UnityEngine;

namespace Core
{
    public class RoundOverDisplayer : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameObject roundOverPanel;
        [SerializeField] private TextMeshProUGUI roundOverTextObject;
        [SerializeField] private TextMeshProUGUI strongerMicrobe;
        [SerializeField] private TextMeshProUGUI moreMicrobe;
        [SerializeField] private TextMeshProUGUI lessMolecule;

        private bool hasWon;
        private string roundOverText;

        private RoundHandler roundHandler;
        private ScoreCalculator scoreCalculator;
        private RoundAmplifierHandler roundAmplifierHandler;

        private float previousTimeScale;

        private void OnEnable()
        {
            IngameEvents.RoundWon += HandleRoundWon;
            IngameEvents.RoundLost += HandleRoundLost;
        }

        private void Start()
        {
            roundHandler = this.Inject<RoundHandler>();
            scoreCalculator = this.Inject<ScoreCalculator>();
            roundAmplifierHandler = this.Inject<RoundAmplifierHandler>();
        }

        private void HandleRoundWon()
        {
            roundOverText = "Round Won!";
            hasWon = true;
            DisplayResult();
        }

        private void HandleRoundLost()
        {
            roundOverText = "Round Lost";
            hasWon = false;
            DisplayResult();
        }

        public void DisplayResult()
        {
            previousTimeScale = Time.timeScale;
            Time.timeScale = 0f;
            roundOverPanel.SetActive(true);
            roundOverTextObject.text = roundOverText;
            strongerMicrobe.text = $"{roundAmplifierHandler.GetDefaultStrongerMicrobeValue().AsRoundStr(1)}%";
            moreMicrobe.text = $"{roundAmplifierHandler.CurrentMicrobeSpawnRate.value.AsRoundStr(1)}%";
            lessMolecule.text = $"{roundAmplifierHandler.CurrentMoleculeSpawnRate.value.AsRoundStr(1)}%";
        }

        public void Continue()
        {
            roundOverPanel.SetActive(false);
            IngameEvents.RoundContinuedByPlayer.Invoke();
            Time.timeScale = previousTimeScale;
        }

        public void Back()
        {

        }


        private void OnDisable()
        {
            IngameEvents.RoundWon -= HandleRoundWon;
            IngameEvents.RoundLost -= HandleRoundLost;
        }
    }
}
