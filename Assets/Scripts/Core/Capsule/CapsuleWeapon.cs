using Common;
using Core.Generated;
using UnityEngine;

namespace Core
{
    public class CapsuleWeapon : MonoBehaviour
    {
        private StatSOContainer statSOContainer;
        private StatsHandler statsHandler;

        private float damage;

        private void Start()
        {
            statSOContainer = SOContainerContainer.StatSOContainer;
            statsHandler = GetComponentInParent<IStatsHandlerHolder>().GetStatsHandler();
            statsHandler.valueChanged += HandleStatChanged;
            HandleStatChanged();
        }


        private void OnTriggerEnter(Collider collider)
        {
            if (!collider.gameObject.TryGetComponent<IDamageable>(out var damagable))
            {
                return;
            }

            damagable.Damage(damage);
        }

        private void HandleStatChanged()
        {
            statsHandler.TryGetAttributeValue(statSOContainer.damage, out var damage);
            this.damage = (float)damage;
        }
    }
}
