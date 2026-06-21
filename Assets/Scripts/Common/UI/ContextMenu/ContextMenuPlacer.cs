using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Common
{
    public class ContextMenuPlacer : MonoBehaviour
    {
        [SerializeField] private ContextMenu contextMenu;

        public void PositionContextMenu(RectTransform target)
        {
            contextMenu.gameObject.SetActive(true);

            Vector3[] corners = new Vector3[4];
            target.GetWorldCorners(corners);

            Vector3 bottomCenter = (corners[0] + corners[3]) * 0.5f;

            Vector3 localPoint = contextMenu.transform.parent.InverseTransformPoint(bottomCenter);

            contextMenu.transform.localPosition = localPoint;
        }

        public void OpenContextMenu(List<ContextMenuItem> menuItems)
        {
            foreach (var item in menuItems)
            {
                if (item.onClick != null)
                {
                    item.onClick += contextMenu.Close;
                }
            }
            contextMenu.Open(menuItems);
        }
    }
}
