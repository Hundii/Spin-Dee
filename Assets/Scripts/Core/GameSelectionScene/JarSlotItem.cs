using Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class JarSlotItem : MonoBehaviour, ISlotMachineItem
    {
        [SerializeField] private Image icon;

        private JarSO jarSO;
        private TextMeshProUGUI descriptionText;

        private GameSelectionSceneHandler selectionHandler;
        public void Init(JarSO jarSO, TextMeshProUGUI descriptionText)
        {
            selectionHandler = this.Inject<GameSelectionSceneHandler>();
            this.jarSO = jarSO;
            this.descriptionText = descriptionText;

            icon.sprite = jarSO.icon;
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
            descriptionText.text = jarSO.description;
        }

        public void HandleUserSelected()
        {
            selectionHandler.SelectJar(jarSO);
        }

        public void HandleUserDeselected()
        {
            selectionHandler.DeselectJar(jarSO);
        }
    }
}
