using System.Security.Cryptography;
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
            Debug.Log($"Spin landed on {name}");
        }

        public void HandleUserSelected()
        {
            Debug.Log(boosterSO.GetDisplayString());
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
    }
}
