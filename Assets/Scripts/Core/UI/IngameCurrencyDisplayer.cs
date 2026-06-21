using Common;
using TMPro;
using UnityEngine;

namespace Core
{
    public class IngameCurrencyDisplayer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI moleculeMaterialText;

        private IngameInventory ingameInventory;
        private void Start()
        {
            ingameInventory = this.Inject<IngameInventory>();
        }

        private void OnEnable()
        {
            GlobalEvents.MoleculeMaterialHarvestedByPlayer.RegisterListener(HandleMoleculeMaterialHarvestedByPlayer);
        }

        public void HandleMoleculeMaterialHarvestedByPlayer(float amount)
        {
            moleculeMaterialText.text = ingameInventory.GetCurrentMoleculeMaterial().ToString();
        }
        private void OnDisable()
        {
            GlobalEvents.MoleculeMaterialHarvestedByPlayer.UnRegisterListener(HandleMoleculeMaterialHarvestedByPlayer);

        }
    }
}
