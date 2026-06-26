using Common;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
     // This component is a mess, should have created 3 panels instead
    public class RoundOverDisplayer : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameObject roundOverPanel;
        [SerializeField] private GameObject infoTextBox;
        [SerializeField] private TextMeshProUGUI roundOverTextObject;
        [SerializeField] private TextMeshProUGUI strongerMicrobe;
        [SerializeField] private TextMeshProUGUI moreMicrobe;
        [SerializeField] private TextMeshProUGUI lessMolecule;
        [SerializeField] private GameObject continueButton;
        [SerializeField] private GameObject backButton;
        [SerializeField] private GameObject yaayButton;


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
            IngameEvents.GameWon += HandleGameWon;
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
            infoTextBox.SetActive(true);
            continueButton.SetActive(true);
            DisplayResult();
        }

        private void HandleGameWon()
        {
            roundOverText = "Game Won!";
            hasWon = true;
            infoTextBox.SetActive(false);
            continueButton.SetActive(false);
            backButton.SetActive(false);
            yaayButton.SetActive(true);
            DisplayResult();
        }

        private void HandleRoundLost()
        {
            roundOverText = "Round Lost";
            infoTextBox.SetActive(false);
            continueButton.SetActive(false);
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

        public void YaayButtonPressed()
        {
            yaayButton.SetActive(false);
            Back();
        }

        public void Back()
        {
            SceneManager.LoadScene("GameSelectionScene");
            Time.timeScale = previousTimeScale;
        }


        private void OnDisable()
        {
            IngameEvents.RoundWon -= HandleRoundWon;
            IngameEvents.RoundLost -= HandleRoundLost;
            IngameEvents.GameWon -= HandleGameWon;
        }
    }
}
