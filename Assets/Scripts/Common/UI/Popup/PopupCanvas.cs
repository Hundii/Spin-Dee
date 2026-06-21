using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Common
{
    public class PopupCanvas : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform overlay;

        private List<IPopupPanel> popupPanels;

        private void Awake()
        {
            popupPanels = GetComponentsInChildren<IPopupPanel>().ToList();
            foreach (var panel in popupPanels)
            {
                panel.OnClose += OnPopupClosed;
            }
        }
                        
        public bool OpenPopup<T1,T2>(T1 data, out T2 reference) where T2 : class, IPopupPanel<T1>
        {
            foreach (var panel in popupPanels)
            {
                if (panel.GetType().IsAssignableFrom(typeof(T2)))
                {
                    ActivateOverlay(true);
                    reference = panel as T2;
                    reference.Open(data);
                    return true;
                }
            }
            reference = null;
            return false;
        }

        public bool OpenPopup<T>(out T reference) where T : class
        {
            foreach (var panel in popupPanels)
            {
                if (panel.GetType().IsAssignableFrom(typeof(T)))
                {
                    ActivateOverlay(true);
                    reference = panel as T;
                    panel.Open();
                    return true;
                }
            }
            reference = null;
            return false;
        }

        public void ClosePopups(bool forceClose = false)
        {
            foreach (var popupPanel in popupPanels)
            {
                if (forceClose)
                {
                    popupPanel.Close();
                }
                else if(popupPanel.IsOpened())
                {
                    popupPanel.Close();
                }
            }
            ActivateOverlay(false);
        }

        public void OnPopupClosed(IPopupPanel _)
        {
            ActivateOverlay(false);
        }
        private void ActivateOverlay(bool active)
        {
            if (overlay == null)
            {
                return;
            }
            overlay.gameObject.SetActive(active);
        }
    }
}
