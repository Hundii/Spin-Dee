using Common;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(menuName = Utility.DefaultScriptableObjectPrefix + "Loot/Drop/Currency")]
    public class CurrencyLootDrop : LootDrop
    {
        public CurrencySO currency;
        
        public override void Activate(float amount)
        {
            base.Activate(amount);
            var stackingInventory = DIContainer.Inject<PlayerInventory>().StackingInventory;
            stackingInventory.AddItems(currency, (int)amount);
        }
    }
}
