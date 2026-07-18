using Common;
using TMPro;
using UnityEngine;

namespace Core
{
    public class IngameCurrencyDisplayer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI moleculeMaterialText;

        private IngameInventory ingameInventory;

        private SubscriptionList subscriptions = new();
        private void Start()
        {
            ingameInventory = this.Inject<IngameInventory>();
            HandleMoleculeMaterialChanged();
        }

        private void OnEnable()
        {
            IngameEvents.PlayerMoleculeMaterialChanged.RegisterListener(new(HandleMoleculeMaterialChanged));
        }

        public void HandleMoleculeMaterialChanged()
        {
            moleculeMaterialText.text = ingameInventory.MoleculeMaterial.AsRoundStr(1);
        }
        private void OnDisable()
        {
            subscriptions.UnsubscribeAll();
        }
    }
}
