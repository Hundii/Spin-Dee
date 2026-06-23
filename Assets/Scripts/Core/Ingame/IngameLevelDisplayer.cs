using Common;
using TMPro;
using UnityEngine;

namespace Core
{
    public class IngameLevelDisplayer : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TextMeshProUGUI currentLevelText;
        [SerializeField] private TextMeshProUGUI currentExperienceText;
        [SerializeField] private TextMeshProUGUI experienceRequirementText;

        private IngameLevelHandler ingameLevelHandler;

        private void OnEnable()
        {
            IngameEvents.ExperienceEarned.RegisterListener(HandleExperienceEarned,true);
        }

        private void Start()
        {
            ingameLevelHandler = this.Inject<IngameLevelHandler>();
            UpdateTexts();
        }

        public void HandleExperienceEarned(float amount)
        {
            UpdateTexts();
        }

        public void UpdateTexts()
        {
            currentLevelText.text = ingameLevelHandler.GetCurrentLevel().ToString();
            currentExperienceText.text = ingameLevelHandler.GetCurrentExperience().ToString();
            experienceRequirementText.text = ingameLevelHandler.GetLevelUpRequirement().ToString();
        }

        private void OnDisable()
        {
            IngameEvents.ExperienceEarned.UnRegisterListener(HandleExperienceEarned);
        }
    }
}
