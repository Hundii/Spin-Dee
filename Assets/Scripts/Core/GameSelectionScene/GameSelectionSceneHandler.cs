using Common;
using Core.Generated;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Core
{
    public class GameSelectionSceneHandler : MonoBehaviour, INonPersistentManager
    {
        // Would have been wise to create a script for these but whatever

        [Header("References")]
        [Header("Capsule")]
        [SerializeField] private SlotMachineSlot capsuleSlot;
        [SerializeField] private TextMeshProUGUI capsuleDescription;
        [SerializeField] private GameObject capsuleSlotItemPrefab;

        [Header("Liquid")]
        [SerializeField] private SlotMachineSlot liquidSlot;
        [SerializeField] private TextMeshProUGUI liquidDescription;
        [SerializeField] private GameObject liquidSlotItemPrefab;

        [Header("Jar")]
        [SerializeField] private SlotMachineSlot jarSlot;
        [SerializeField] private TextMeshProUGUI jarDescription;
        [SerializeField] private GameObject jarSlotItemPrefab;


        private GameSelectionSceneData selectionSceneData = new();

        private void Start()
        {
            SpawnItems();
        }

        private void SpawnItems()
        {
            var capsuleSOContainer = SOContainerContainer.CapsuleSOContainer;
            var jarSOContainer = SOContainerContainer.JarSOContainer;
            var liquidSOContainer = SOContainerContainer.LiquidSOContainer;

            var capsuleSOArray = capsuleSOContainer.capsuleSOArray.OrderByDescending(x => x.isStarter).ToArray();
            var liquidSOArray = liquidSOContainer.liquidSOArray.OrderByDescending(x => x.isStarter).ToArray();
            var jarSOArray = jarSOContainer.jarSOArray.OrderByDescending(x => x.isStarter).ToArray();

            var capsuleItems = capsuleSlot.RegisterItems(capsuleSOArray.Select(x => capsuleSlotItemPrefab).ToArray());
            var liquidItems = liquidSlot.RegisterItems(liquidSOArray.OrderBy(x => x.isStarter).Select(x => liquidSlotItemPrefab).ToArray());
            var jarItems = jarSlot.RegisterItems(jarSOArray.OrderBy(x => x.isStarter).Select(x => jarSlotItemPrefab).ToArray());

            for (int i = 0; i < capsuleItems.Count; i++)
            {
                var slotItem = capsuleItems[i].GetGameObject().GetComponent<CapsuleSlotItem>();
                slotItem.Init(capsuleSOArray[i], capsuleDescription);
                if (i == 0)
                {
                    slotItem.UpdateDescriptionText();
                }
            }

            for (int i = 0; i < liquidItems.Count; i++)
            {
                var slotItem = liquidItems[i].GetGameObject().GetComponent<LiquidSlotItem>();
                slotItem.Init(liquidSOArray[i], liquidDescription);
                if (i == 0)
                {
                    slotItem.UpdateDescriptionText();
                }
            }

            for (int i = 0; i < jarItems.Count; i++)
            {
                var slotItem = jarItems[i].GetGameObject().GetComponent<JarSlotItem>();
                slotItem.Init(jarSOArray[i], jarDescription);
                if (i == 0)
                {
                    slotItem.UpdateDescriptionText();
                }
            }

            capsuleSlot.ShowSelectButton();
            jarSlot.ShowSelectButton();
            liquidSlot.ShowSelectButton();

        }

        public void SelectCapsule(CapsuleSO capsuleSO)
        {
            selectionSceneData.capsuleStatSO = capsuleSO;
        }

        public void SelectLiquid(LiquidSO liquidSO)
        {
            selectionSceneData.liquidSO = liquidSO;
        }

        public void SelectJar(JarSO jarSO)
        {
            selectionSceneData.jarSO = jarSO;
        }
    }
}
