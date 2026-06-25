using Common;
using Core.Generated;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core
{
    public class BoosterSlotMachine : MonoBehaviour, INonPersistentManager
    {
        [SerializeField] private int numberOfBoosterPerSlot = 8;
        [SerializeField] private float[] rarityWeights;

        [Header("References")]
        [SerializeField] private Booster boosterPrefab;
        [SerializeField] private GameObject content;

        private SlotMachineSlot[] slots;
        private BoosterSOContainer boosterSOContainer;

        public GameEvent SelectionFinished { get; private set; } = new();

        private void Awake()
        {
            slots = GetComponentsInChildren<SlotMachineSlot>(true);

            foreach (var slot in slots)
            {
                slot.UserSelectedSlot.RegisterListener(HandleSlotSelected);
            }

            boosterSOContainer = SOContainerContainer.BoosterSOContainer;
        }

        public void Open()
        {
            content.SetActive(true);
            GenerateBoosters();
            Spin();
        }

        public void GenerateBoosters()
        {
            foreach (var slot in slots)
            {
                slot.Clear();
            }
            for (int slot = 0; slot < slots.Length; slot++)
            {
                List<BoosterSO> boosterSOList = new();
                for (int i = 0; i < numberOfBoosterPerSlot; i++)
                {
                    GeneralRarity rarity = (GeneralRarity)RandomUtility.RandomWeightedTable(rarityWeights);
                    BoosterSO booster =
                        RandomUtility.RandomElement(
                            boosterSOContainer.boosterSOArray.Where(x => x.generalRarity == rarity && x != boosterSOContainer.baseBooster)
                            .ToList()
                            );
                    boosterSOList.Add(booster);
                }
                var slotItems = slots[slot].RegisterItems(boosterSOList.Select(x => boosterPrefab.gameObject).ToArray());
                InitBoosters(slotItems, boosterSOList);
            }
        }

        private void InitBoosters(List<ISlotMachineItem> slotMachineItems, List<BoosterSO> boosters)
        {
            for (int i = 0; i < slotMachineItems.Count; i++)
            {
                (slotMachineItems[i] as Booster).Init(boosters[i]);
            }
        }

        public void Spin()
        {
            foreach (var slot in slots)
            {
                slot.Spin();
            }
        }

        private void HandleSlotSelected(SlotMachineSlot slot)
        {
            content.SetActive(false);
            SelectionFinished.Invoke();
        }
    }
}
