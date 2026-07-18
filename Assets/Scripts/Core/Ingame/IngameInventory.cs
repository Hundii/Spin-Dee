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

        private SubscriptionList subscriptions = new();

        private void OnEnable()
        {
            subscriptions.Add(IngameEvents.MoleculeMaterialHarvestedByPlayer.RegisterListener(new(HandleMoleculeMaterialHarvestedByPlayer)));
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
            subscriptions.UnsubscribeAll();
        }
    }
}
