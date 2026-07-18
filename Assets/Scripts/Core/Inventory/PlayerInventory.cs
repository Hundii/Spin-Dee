using Common;
using Common.Saving.Flexible;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class PlayerInventory : MonoBehaviour, ILoadingSceneEntity, IPersistentManager
    {
        public GenericStackingInventory StackingInventory { get; private set; } = new();

        public const string FileName = "Inventory";

        public Action<ILoadingSceneEntity> _onReady;

        void ILoadingSceneEntity.OnCreation(Action<ILoadingSceneEntity> onReady)
        {
            _onReady = onReady;
        }

        private SubscriptionList subscriptionList = new();

        private void OnEnable()
        {
            subscriptionList.Add(GlobalEvents.GameSaved.RegisterListener(new(Save)));
            subscriptionList.Add(GlobalEvents.GameLoaded.RegisterListener(new(Load)));
        }

        private void Start()
        {
            Load();
            _onReady.Invoke(this);
        }

        public void Load()
        {
            StackingInventory.Load(FileName);
        }

        public void SaveToDisk()
        {
            Save();
            FlexibleSaveSystem.SaveFiles(new List<string>() { FileName });
            GlobalEvents.PlayerInventorySavedToDisk.Invoke();
        }
        public void Save()
        {
            StackingInventory.Save(FileName);
        }

        private void OnDisable()
        {
            subscriptionList.UnsubscribeAll();
        }
    }
}
