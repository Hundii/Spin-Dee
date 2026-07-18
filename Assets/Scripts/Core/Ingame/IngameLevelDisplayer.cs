using Common;
using TMPro;
using UnityEngine;

namespace Core
{
    public class IngameLevelDisplayer : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TextMeshProUGUI experienceText;

        private IngameLevelHandler ingameLevelHandler;

        private SubscriptionList subscriptions = new();
        private void OnEnable()
        {
            subscriptions.Add(IngameEvents.ExperienceEarned.RegisterListener(new(HandleExperienceEarned)));
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
            var currentExp = ingameLevelHandler.GetCurrentExperience();
            var requiredExp = ingameLevelHandler.GetLevelUpRequirement();
            experienceText.text = $"{currentExp.AsRoundStr(1)} / {requiredExp.AsRoundStr(1)}";
        }

        private void OnDisable()
        {
            subscriptions.UnsubscribeAll();
        }
    }
}
