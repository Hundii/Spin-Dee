using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public abstract class PopupPanelBase<T> : MonoBehaviour, IPopupPanel<T>
    {
        [Header("References")]
        [SerializeField] private GameObject content;
        public Action<IPopupPanel> OnClose { get; set; }
        public virtual void Open()
        {
            content.SetActive(true);
        }
        public virtual void Open(T data)
        {
            Open();
        }
        public virtual void Close()
        {
            content.SetActive(false);
            OnClose.Invoke(this);
        }
        public virtual bool IsOpened()
        {
            return content.activeSelf;
        }
    }

    public abstract class PopupPanelBase : MonoBehaviour, IPopupPanel
    {
        [Header("References")]
        [SerializeField] private GameObject content;
        public Action<IPopupPanel> OnClose { get; set; }
        public virtual void Open()
        {
            content.SetActive(true);
        }

        public virtual void Close()
        {
            content.SetActive(false);
            OnClose.Invoke(this);
        }

        public virtual bool IsOpened()
        {
            return content.activeSelf;
        }
    }
}
