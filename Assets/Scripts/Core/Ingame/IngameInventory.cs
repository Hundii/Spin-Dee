using Common;
using UnityEngine;

namespace Core
{
    public class IngameInventory : MonoBehaviour, INonPersistentManager
    {
        private float _moleculeMaterial;
        public float MoleculeMaterial
        {
            get
            {
                return _moleculeMaterial;
            }
            private set
            {
                _moleculeMaterial = value;
                IngameEvents.PlayerMoleculeMaterialChanged.Invoke(value);
            }
        }

        private void OnEnable()
        {
            IngameEvents.MoleculeMaterialHarvestedByPlayer.RegisterListener(HandleMoleculeMaterialHarvestedByPlayer);
        }

        public void HandleMoleculeMaterialHarvestedByPlayer(float amount)
        {
            MoleculeMaterial += amount;
        }

        public bool HasEnoughMaterial(float amount)
        {
            return MoleculeMaterial >= amount;
        }

        public void AddMoleculeMaterial(float amount)
        {
            MoleculeMaterial += amount;
        }

        private void OnDisable()
        {
            IngameEvents.MoleculeMaterialHarvestedByPlayer.UnRegisterListener(HandleMoleculeMaterialHarvestedByPlayer);
        }
    }
}
