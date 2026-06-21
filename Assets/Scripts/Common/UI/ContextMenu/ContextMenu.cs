using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class ContextMenu : MonoBehaviour
    {
        [SerializeField] private ContextMenuItemUI contextMenuItemUI;
        [SerializeField] private Transform itemsContainer;

        private UISpawner<ContextMenuItem, ContextMenuItemUI> spawner;

        private void Awake()
        {
            spawner = new(itemsContainer,contextMenuItemUI);
        }

        public void Open(List<ContextMenuItem> items)
        {
            spawner.PoolRefreshItems(items);
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }
    }
}
