using Common;
using TMPro;
using UnityEngine;

namespace Core
{
    public class CapsuleLevelDisplayer : MonoBehaviour
    {
        [SerializeField] private TextMeshPro levelText;

        private void OnEnable()
        {
            IngameEvents.LeveledUp.RegisterListener(UpdateLevelDisplay);
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
            IngameEvents.LeveledUp.UnRegisterListener(UpdateLevelDisplay);
        }
    }
}
