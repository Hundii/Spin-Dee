using Common;
using DanielLochner.Assets.SimpleScrollSnap;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(SimpleScrollSnap))]
    public class SlotMachineSlot : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform container;
        [SerializeField] private GameObject selectButton;

        private SimpleScrollSnap scrollSnap;

        private List<ISlotMachineItem> slotItems;

        private int selectedIndex;

        public GameEvent<SlotMachineSlot> UserSelectedSlot { get; private set; } = new();

        private void Awake()
        {
            scrollSnap = GetComponent<SimpleScrollSnap>();
            scrollSnap.OnPanelSelected.AddListener(HandlePanelSelected);
            scrollSnap.UseUnscaledTime = true;
            selectButton.SetActive(false);
        }

        public void Spin()
        {
            scrollSnap.Velocity += Random.Range(25000, 50000) * Vector2.up;
        }

        public void Clear()
        {
            var numberOfPanels = scrollSnap.NumberOfPanels;
            for (int i = 0; i < numberOfPanels; i++)
            {
                scrollSnap.RemoveFromBack();
            }
            slotItems = new();
            selectedIndex = -1;
            selectButton.SetActive(false);
        }

        public List<ISlotMachineItem> RegisterItems(ICollection<GameObject> items)
        {
            foreach (var item in items)
            {
                scrollSnap.AddToBack(item);
            }
            slotItems = new();
            for (int i = 0; i < scrollSnap.NumberOfPanels; i++)
            {
                slotItems.Add(scrollSnap.Panels[i].gameObject.GetComponent<ISlotMachineItem>());
            }
            return slotItems;
        }

        public void HandlePanelSelected(int _)
        {
            slotItems.ElementAt(scrollSnap.CenteredPanel).HandleSpinLanded();
            selectedIndex = scrollSnap.CenteredPanel;
            selectButton.SetActive(true);
        }

        public void SelectItem()
        {
            slotItems[selectedIndex].HandleUserSelected();
            selectButton.SetActive(false);
            UserSelectedSlot.Invoke(this);
        }

        public void ShowSelectButton()
        {
            selectButton.SetActive(true);
        }

        public ISlotMachineItem GetSelectedItem()
        {
            return slotItems[selectedIndex];
        }
    }
}
