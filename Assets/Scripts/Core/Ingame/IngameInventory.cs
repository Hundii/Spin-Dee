using Common;
using UnityEngine;

namespace Core
{
    public class IngameInventory : MonoBehaviour, INonPersistentManager
    {
        private float moleculeMaterial;

        private void OnEnable()
        {
            IngameEvents.MoleculeMaterialHarvestedByPlayer.RegisterListener(HandleMoleculeMaterialHarvestedByPlayer);
        }

        public void HandleMoleculeMaterialHarvestedByPlayer(float amount)
        {
            moleculeMaterial += amount;
        }

        public float GetCurrentMoleculeMaterial()
        {
            return moleculeMaterial;
        }

        private void OnDisable()
        {
            IngameEvents.MoleculeMaterialHarvestedByPlayer.UnRegisterListener(HandleMoleculeMaterialHarvestedByPlayer);
        }
    }
}
