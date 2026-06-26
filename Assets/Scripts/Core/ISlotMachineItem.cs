using UnityEngine;

namespace Core
{
    public interface ISlotMachineItem
    {
        public GameObject GetGameObject();
        public void HandleSpinLanded();
        public void HandleUserSelected();
        public void HandleUserDeselected();
    }
}
