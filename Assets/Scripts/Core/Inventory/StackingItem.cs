using UnityEngine;

namespace Core
{
    public class StackingItem
    {
        private ItemDefinitionSO itemDefinition;
        public int Count { get; set; }
        public StackingItem(ItemDefinitionSO itemDefinition)
        {
            this.itemDefinition = itemDefinition;
            Count = 1;
        }

        public ItemDefinitionSO GetItemDefinition()
        {
            return itemDefinition;
        }
        public void AddItems(int amount)
        {
            Count += amount;
        }
        public bool IsDead()
        {
            return Count <= 0;
        }
    }

    public struct StackingItem<T> where T : ItemDefinitionSO
    {
        public T itemDefinition;
        public int Count { get; set; }
        public StackingItem(T itemDefinition)
        {
            this.itemDefinition = itemDefinition;
            Count = 1;
        }

        public StackingItem(StackingItem otherItem)
        {
            itemDefinition = otherItem.GetItemDefinition() as T;
            Count = otherItem.Count;
        }

        public bool IsDead()
        {
            return Count <= 0;
        }
    }
}
