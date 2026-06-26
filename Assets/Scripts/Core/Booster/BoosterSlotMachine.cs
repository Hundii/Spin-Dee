using Common;
using Core.Generated;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class BoosterSlotMachine : MonoBehaviour, INonPersistentManager
    {
        [SerializeField] private int numberOfBoosterPerSlot = 8;
        [SerializeField] private float[] rarityWeights;
        [SerializeField] private int baseRespinCost = 5;

        [Header("References")]
        [SerializeField] private Booster boosterPrefab;
        [SerializeField] private GameObject content;
        [SerializeField] private Button respinButton;
        [SerializeField] private TextMeshProUGUI respinCostText;

        private float currentRespinCost;

        private SlotMachineSlot[] slots;
        private BoosterSOContainer boosterSOContainer;

        private IngameInventory ingameInventory;

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

        private void OnEnable()
        {
            IngameEvents.PlayerMoleculeMaterialChanged += EnableDisableSpinButton;
        }

        private void Start()
        {
            ingameInventory = this.Inject<IngameInventory>();
        }

        public void Open()
        {
            content.SetActive(true);
            GenerateBoosters();
            currentRespinCost = baseRespinCost;
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
                slot.SpinEnded.RegisterOneTimeListener(HandleSpinEnded);
            }
            respinButton.interactable = false;
            respinCostText.text = currentRespinCost.AsRoundStr(1);
        }

        public void Respin()
        {
            if (ingameInventory.HasEnoughMaterial(currentRespinCost))
            {
                ingameInventory.AddMoleculeMaterial(-currentRespinCost);
                currentRespinCost += baseRespinCost;
                Spin();
            }
        }

        private void HandleSpinEnded()
        {
            EnableDisableSpinButton();
        }

        private void HandleSlotSelected(SlotMachineSlot slot)
        {
            content.SetActive(false);
            SelectionFinished.Invoke();
        }

        private void EnableDisableSpinButton()
        {
            if (ingameInventory.HasEnoughMaterial(currentRespinCost))
            {
                respinButton.interactable = true;
                return;
            }
            respinButton.interactable = false;
        }

        private void OnDisable()
        {
            IngameEvents.PlayerMoleculeMaterialChanged -= EnableDisableSpinButton;
        }
    }
}
