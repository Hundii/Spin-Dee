using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class UISpawner<T1, T2> where T2 : MonoBehaviour, IUISpawnable<T1>
    {
        public Transform ItemContainer { get; set; }
        public List<T1> Items { get; set; }
        public T2 Prefab { get; set; }

        private List<GameObject> spawnedObjects = new();
        public List<T2> SpawnedPrefabs { get; set; } = new();

        public Transform ItemParent { get; set; }

        public UISpawner(Transform itemContainer, T2 prefab, List<T1> items = null, Transform itemParent = null)
        {
            ItemContainer = itemContainer;
            Prefab = prefab;
            Items = items ?? new();
            ItemParent = itemParent;
        }

        protected virtual T2 SpawnItem()
        {
            Transform itemParent = ItemContainer;
            if (ItemParent != null)
            {
                itemParent = Object.Instantiate(ItemParent, ItemContainer, false);
                spawnedObjects.Add(itemParent.gameObject);
            }
            var obj = Object.Instantiate(Prefab, itemParent, false);
            return obj;
        }
        public void DestroyItems()
        {
            foreach (var item in SpawnedPrefabs)
            {
                Object.Destroy(item);
            }
            foreach (var item in spawnedObjects)
            {
                Object.Destroy(item);
            }
            SpawnedPrefabs = new();
            spawnedObjects = new();
        }

        public void PoolRefreshItems(List<T1> newItems)
        {
            if (newItems == null)
            {
                return;
            }
            foreach (var spawnedItem in SpawnedPrefabs)
            {
                spawnedItem.gameObject.SetActive(false);
            }
            int spawnedItemCount = SpawnedPrefabs.Count;
            for (int i = 0; i < newItems.Count - spawnedItemCount; i++)
            {
                var obj = SpawnItem();
                SpawnedPrefabs.Add(obj);
            }
            for (int i = 0; i < newItems.Count; i++)
            {
                SpawnedPrefabs[i].Init(newItems[i]);
                SpawnedPrefabs[i].gameObject.SetActive(true);
            }
        }
        public List<T2> RespawnItems()
        {
            DestroyItems();
            PoolRefreshItems(Items);
            return SpawnedPrefabs;
        }

        public void Clear()
        {
            DestroyItems();
            Items = new();
        }
    }


    public class UISpawnerWithIndividualData<T1, T2, T3> where T2 : MonoBehaviour, IUISpawnable<T1, T3>
    {
        public Transform ItemContainer { get; set; }
        public T2 Prefab { get; set; }
        public List<(T1, T3)> ItemsWithData { get; set; }

        private List<GameObject> spawnedObjects = new();
        public List<T2> SpawnedPrefabs { get; set; } = new();

        public Transform ItemParent { get; set; }

        public UISpawnerWithIndividualData(Transform itemContainer, T2 prefab, List<(T1, T3)> itemsWithData = null, Transform itemParent = null)
        {
            ItemContainer = itemContainer;
            Prefab = prefab;
            ItemsWithData = itemsWithData ?? new();
            ItemParent = itemParent;
        }

        private T2 SpawnItem()
        {
            Transform itemParent = ItemContainer;
            if (ItemParent != null)
            {
                itemParent = Object.Instantiate(ItemParent, ItemContainer, false);
                spawnedObjects.Add(itemParent.gameObject);
            }
            var obj = Object.Instantiate(Prefab, itemParent, false);
            return obj;
        }

        public bool RefreshItems(List<(T1, T3)> itemsWithData = null)
        {
            if (itemsWithData != null)
            {
                ItemsWithData = itemsWithData;
            }
            if (ItemsWithData.Count != SpawnedPrefabs.Count)
            {
                return false;
            }
            for (int i = 0; i < ItemsWithData.Count; i++)
            {
                SpawnedPrefabs[i].Refresh(ItemsWithData[i].Item1, ItemsWithData[i].Item2);
            }
            return true;
        }

        public void PoolRefreshItems(List<(T1, T3)> newItemsWithData)
        {
            if (newItemsWithData == null)
            {
                return;
            }
            foreach (var spawnedItem in SpawnedPrefabs)
            {
                spawnedItem.gameObject.SetActive(false);
            }
            int spawnedItemCount = SpawnedPrefabs.Count;
            for (int i = 0; i < newItemsWithData.Count - spawnedItemCount; i++)
            {
                var obj = SpawnItem();
                SpawnedPrefabs.Add(obj);
            }
            for (int i = 0; i < newItemsWithData.Count; i++)
            {
                SpawnedPrefabs[i].Init(newItemsWithData[i]);
                SpawnedPrefabs[i].gameObject.SetActive(true);
            }
        }

        public void DestroyItems()
        {
            foreach (var item in SpawnedPrefabs)
            {
                Object.Destroy(item.gameObject);
            }
            foreach (var item in spawnedObjects)
            {
                Object.Destroy(item);
            }
            SpawnedPrefabs = new();
            spawnedObjects = new();
        }

        public List<T2> RespawnItems()
        {
            DestroyItems();
            PoolRefreshItems(ItemsWithData);
            return SpawnedPrefabs;
        }

        public void Clear()
        {
            DestroyItems();
            ItemsWithData = new();
        }
    }

    public class UISpawner<T1, T2, T3> : UISpawner<T1, T2> where T2 : MonoBehaviour, IUISpawnable<T1, T3>
    {
        public T3 AdditionalData { get; set; }
        public UISpawner(Transform itemContainer, T2 prefab, T3 additionalData, List<T1> items = null, Transform itemParent = null) :
            base(itemContainer, prefab, items, itemParent)
        {
            AdditionalData = additionalData;
        }

        public void PoolRefreshItems(List<T1> newItems, T3 additionalData)
        {
            if (newItems == null)
            {
                return;
            }
            foreach (var spawnedItem in SpawnedPrefabs)
            {
                spawnedItem.gameObject.SetActive(false);
            }
            int spawnedItemCount = SpawnedPrefabs.Count;
            for (int i = 0; i < newItems.Count - spawnedItemCount; i++)
            {
                var obj = SpawnItem();
                SpawnedPrefabs.Add(obj);
            }
            for (int i = 0; i < newItems.Count; i++)
            {
                SpawnedPrefabs[i].Init(newItems[i], additionalData);
                SpawnedPrefabs[i].gameObject.SetActive(true);
            }
        }
        public bool RefreshItems(T3 additionalData, List<T1> items = null)
        {
            if (items != null)
            {
                Items = items;
            }
            if (Items.Count != SpawnedPrefabs.Count)
            {
                return false;
            }
            for (int i = 0; i < Items.Count; i++)
            {
                SpawnedPrefabs[i].Refresh(Items[i], additionalData);
            }
            return true;
        }
    }
}
