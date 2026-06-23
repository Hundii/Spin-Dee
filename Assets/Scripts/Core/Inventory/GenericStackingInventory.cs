using Common.Saving.Flexible;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core
{
    public class GenericStackingInventory
    {
        private HashSet<StackingItem> items = new();
        public void AddItems(ItemDefinitionSO itemDefinition, int count = 1)
        {
            var item = GetItemWithDefinition(itemDefinition.id);
            if (item == null)
            {
                RegisterItem(new(itemDefinition) { Count = count });
                return;
            }
            item.AddItems(count);
        }

        public bool RegisterItem(StackingItem item)
        {
            return items.Add(item);
        }
        public bool RemoveItems(ItemDefinitionSO itemDefinition, int count = 1)
        {
            var item = GetItemWithDefinition(itemDefinition.id);
            if (item == null)
            {
                return false;
            }
            item.AddItems(-count);
            if (item.IsDead())
            {
                items.Remove(item);
            }
            return true;
        }

        public bool RemoveItems<T>(StackingItem<T> stack) where T : ItemDefinitionSO
        {
            return RemoveItems(stack.itemDefinition, stack.Count);
        }

        public bool RemoveItems<T>(List<StackingItem<T>> stacks) where T : ItemDefinitionSO
        {
            List<StackingItem> items = new();
            foreach (var stack in stacks)
            {
                var item = GetItemWithDefinition(stack.itemDefinition.id);
                if (item == null)
                {
                    return false;
                }
                items.Add(item);
            }
            foreach (var stack in stacks)
            {
                RemoveItems(stack);
            }
            return true;
        }

        public bool HasEnoughItems(ItemDefinitionSO itemDefinition, int count = 1)
        {
            var item = GetItemWithDefinition(itemDefinition.id);
            if (item == null)
            {
                return false;
            }
            return item.Count >= count;
        }

        public void CompletelyRemoveItem(ItemDefinitionSO itemDefinition)
        {
            var item = GetItemWithDefinition(itemDefinition.id);
            items.Remove(item);
        }

        public int GetNumberOfItems(ItemDefinitionSO itemDefinition)
        {
            var item = GetItemWithDefinition(itemDefinition.id);
            if (item == null)
            {
                return 0;
            }
            return item.Count;
        }

        public List<StackingItem<T>> GetItemsFromInheritance<T>() where T : ItemDefinitionSO
        {
            var type = typeof(T);
            var foundItems = items.Where(x => type.IsAssignableFrom(x.GetItemDefinition().GetType())).Select(x => new StackingItem<T>(x)).ToList();
            return foundItems;
        }

        private StackingItem GetItemWithDefinition(string itemDefinition)
        {
            return items.FirstOrDefault(x => x.GetItemDefinition().id == itemDefinition);
        }
        public HashSet<StackingItem> GetAllItems()
        {
            return items;
        }

        public void Save(string fileName)
        {
            List<StackingItemSaveData> saveData = items.Select(x =>
                new StackingItemSaveData() { id = x.GetItemDefinition().id, count = x.Count })
                .ToList();
            FlexibleSaveSystem.Save("PlayerGenericStackingInventory", saveData, fileName);
        }

        public void Load(string fileName)
        {
            List<StackingItemSaveData> saveData = FlexibleSaveSystem.Load<List<StackingItemSaveData>>("PlayerGenericStackingInventory", new(), fileName);
            items = new();
            foreach (var items in saveData)
            {
                AddItems((ItemDefinitionSO)items.id, items.count);
            }
        }
    }
}
