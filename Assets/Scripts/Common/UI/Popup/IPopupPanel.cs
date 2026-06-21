using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public interface IPopupPanel
    {
        public Action<IPopupPanel> OnClose { get; set; }
        public void Close();
        public void Open();
        public bool IsOpened();
    }

    public interface IPopupPanel<T> : IPopupPanel
    {
        public void Open(T param)
        {
            Open();
        }
    }
}
