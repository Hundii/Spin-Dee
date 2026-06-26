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
            HandleMoleculeMaterialChanged();
        }

        private void OnEnable()
        {
            IngameEvents.PlayerMoleculeMaterialChanged.RegisterListener(HandleMoleculeMaterialChanged);
        }

        public void HandleMoleculeMaterialChanged()
        {
            moleculeMaterialText.text = ingameInventory.MoleculeMaterial.AsRoundStr(1);
        }
        private void OnDisable()
        {
            IngameEvents.MoleculeMaterialHarvestedByPlayer.UnRegisterListener(HandleMoleculeMaterialChanged);

        }
    }
}
