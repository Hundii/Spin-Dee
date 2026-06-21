using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Common
{
    public class ContextMenuItemUI : MonoBehaviour, IUISpawnable<ContextMenuItem>
    {
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private Image background;

        private ContextMenuItem item;

        public void Init(ContextMenuItem item)
        {
            this.item = item;
            text.text = item.text;
            text.color = item.textColor;
            background.color = item.color;
        }

        public void OnClick()
        {
            item.onClick.Invoke();
        }
    }
}
