using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class Modifier : MonoBehaviour
    {
        [SerializeField] private Color[] backgorundColorsByRarity;
        [Header("References")]
        [SerializeField] private Image background;
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI detailText;

        private ModifierSO powerupSO;

        public GameObject GetGameObject()
        {
            return gameObject;
        }

        public void HandleSpinLanded()
        {
        }

        public void HandleUserSelected()
        {
            powerupSO.Activate();
        }

        public void Init(ModifierSO powerupSO)
        {
            this.powerupSO = powerupSO;
            background.color = backgorundColorsByRarity[(int)powerupSO.generalRarity];
            icon.sprite = powerupSO.icon;
            UpdateDetailText();
        }

        private void UpdateDetailText()
        {
            detailText.text = $"{GetDisplayString()}";
        }

        public string GetDisplayString()
        {
            return powerupSO.GetDisplayString();
        }

        public void HandleUserDeselected()
        {

        }
    }
}
