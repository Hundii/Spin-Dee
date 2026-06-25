using Common.Saving.Flexible;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core
{
    public class ProfileSlotsManager : MonoBehaviour
    {
        [SerializeField] private GameObject content;
        private List<SaveSlotPanel> slots;
        public void Open()
        {
            slots = GetComponentsInChildren<SaveSlotPanel>(true).ToList();
            for (int i = 0; i < slots.Count; i++)
            {
                string profileName = $"Profile {i + 1}";
                slots[i].Init(profileName,i);
            }
            content.SetActive(true);
        }
    }
}
