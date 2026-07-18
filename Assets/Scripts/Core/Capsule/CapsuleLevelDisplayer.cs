using Common;
using TMPro;
using UnityEngine;

namespace Core
{
    public class CapsuleLevelDisplayer : MonoBehaviour
    {
        [SerializeField] private TextMeshPro levelText;

        private SubscriptionList subscriptions = new();

        private void OnEnable()
        {
           subscriptions.Add(IngameEvents.LeveledUp.RegisterListener(new(UpdateLevelDisplay)));
        }

        private void Start()
        {
            UpdateLevelDisplay(this.Inject<IngameLevelHandler>().GetCurrentLevel());
        }

        public void UpdateLevelDisplay(int level)
        {
            levelText.text = $"Level {level}";
        }

        private void OnDisable()
        {
            subscriptions.UnsubscribeAll();
        }
    }
}
