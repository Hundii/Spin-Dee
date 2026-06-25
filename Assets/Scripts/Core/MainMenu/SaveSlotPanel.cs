using Common.Saving.Flexible;
using System;
using TMPro;
using UnityEngine;

namespace Core
{
    public class SaveSlotPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI profileNameText;
        [SerializeField] private TextMeshProUGUI metadataText;
        [SerializeField] private GameObject deleteButton;

        private string profileName;
        private int index;
        private bool empty;

        public void Init(string profileName, int index)
        {
            var metadata = FlexibleSaveSystem.GetMetadataOfProfile(profileName);
            if (metadata == null)
            {
                SetEmpty(index);
                return;
            }
            metadataText.text = $"Created at: \n{metadata.CreationTime}";
            metadataText.gameObject.SetActive(true);
            this.profileName = profileName;
            profileNameText.text = profileName;

            deleteButton.SetActive(true);
        }

        public void Select()
        {
            if (empty)
            {
                FlexibleSaveSystem.CreateProfile(profileName);
            }
            FlexibleSaveSystem.ChangeProfile(profileName);
        }

        public void DeleteProfile()
        {
            FlexibleSaveSystem.DeleteProfile(profileName);
            SetEmpty(index);
        }

        private void SetEmpty(int index)
        {
            this.index = index;
            profileName = $"Profile {index + 1}";
            profileNameText.text = "Empty Profile";
            empty = true;
            metadataText.gameObject.SetActive(false);
            deleteButton.SetActive(false);
        }
    }
}
