using Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class CapsuleSlotItem : MonoBehaviour, ISlotMachineItem
    {
        [SerializeField] private Image icon;

        private CapsuleSO capsuleSO;
        private TextMeshProUGUI descriptionText;

        private GameSelectionSceneHandler selectionHandler;
        public void Init(CapsuleSO capsuleSO, TextMeshProUGUI descriptionText)
        {
            selectionHandler = this.Inject<GameSelectionSceneHandler>();
            this.capsuleSO = capsuleSO;
            this.descriptionText = descriptionText;

            icon.sprite = capsuleSO.icon;
        }
        public GameObject GetGameObject()
        {
            return gameObject;
        }

        public void HandleSpinLanded()
        {
            UpdateDescriptionText();
        }

        public void UpdateDescriptionText()
        {
            descriptionText.text = capsuleSO.decription;
        }

        public void HandleUserSelected()
        {
            selectionHandler.SelectCapsule(capsuleSO);
        }
    }
}
