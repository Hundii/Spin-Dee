using Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class LiquidSlotItem : MonoBehaviour, ISlotMachineItem
    {
        [SerializeField] private Image icon;

        private LiquidSO liquidSO;
        private TextMeshProUGUI descriptionText;

        private GameSelectionSceneHandler selectionHandler;
        public void Init(LiquidSO liquidSO, TextMeshProUGUI descriptionText)
        {
            selectionHandler = this.Inject<GameSelectionSceneHandler>();
            this.liquidSO = liquidSO;
            this.descriptionText = descriptionText;

            icon.sprite = liquidSO.icon;
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
            descriptionText.text = liquidSO.description;
        }

        public void HandleUserSelected()
        {
            selectionHandler.SelectLiquid(liquidSO);
        }

        public void HandleUserDeselected()
        {
            selectionHandler.DeselectLiquid(liquidSO);
        }
    }
}
