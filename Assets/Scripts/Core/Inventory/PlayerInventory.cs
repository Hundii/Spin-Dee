using Common;
using Common.Saving.Flexible;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class PlayerInventory : MonoBehaviour, ILoadingSceneEntity
    {
        public GenericStackingInventory StackingInventory { get; private set; } = new();

        public const string FileName = "Inventory";

        public Action<UnityEngine.Object> _onReady;

        void ILoadingSceneEntity.OnCreation(Action<UnityEngine.Object> onReady)
        {
            _onReady = onReady;
        }

        private void OnEnable()
        {
            GlobalEvents.GameSaved.RegisterListener(Save);
            GlobalEvents.GameLoaded.RegisterListener(Load);
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
            GlobalEvents.GameSaved.UnRegisterListener(Save);
            GlobalEvents.GameLoaded.UnRegisterListener(Load);
        }
    }
}
