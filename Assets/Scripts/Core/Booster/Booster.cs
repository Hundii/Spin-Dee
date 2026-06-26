using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class Booster : MonoBehaviour, ISlotMachineItem
    {
        [SerializeField] private Color[] backgorundColorsByRarity;
        [Header("References")]
        [SerializeField] private Image background;
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI detailText;

        private BoosterSO boosterSO;

        public GameObject GetGameObject()
        {
            return gameObject;
        }

        public void HandleSpinLanded()
        {
            if (boosterSO.generalRarity == GeneralRarity.Tier4)
            {
                WebGLLocalStorage.Save();
            }
        }

        public void HandleUserSelected()
        {
            boosterSO.Activate();
        }

        public void Init(BoosterSO boosterSO)
        {
            this.boosterSO = boosterSO;
            background.color = backgorundColorsByRarity[(int)boosterSO.generalRarity];
            icon.sprite = boosterSO.icon;
            UpdateDetailText();
        }

        private void UpdateDetailText()
        {
            detailText.text = $"{GetDisplayString()}";
        }

        public string GetDisplayString()
        {
            return boosterSO.GetDisplayString();
        }

        public void HandleUserDeselected()
        {
            
        }
    }
}
