using Common;
using Core.Generated;
using UnityEngine;

namespace Core
{
    public class CapsuleWeapon : MonoBehaviour
    {
        [SerializeField] private float attackCooldown = 1f;
        private StatSOContainer statSOContainer;
        private StatsHandler statsHandler;

        private float damage;
        private float currentCooldown;

        public GameEvent<CapsuleWeapon> WeaponHit { get; private set; } = new();

        private void Start()
        {
            statSOContainer = SOContainerContainer.StatSOContainer;

            currentCooldown = attackCooldown;

            statsHandler = GetComponentInParent<IStatsHandlerHolder>().GetStatsHandler();
            statsHandler.valueChanged += HandleStatChanged;
            HandleStatChanged();

        }

        private void Update()
        {
            currentCooldown -= Time.deltaTime;
            if (currentCooldown <= 0f)
            {
                currentCooldown = 0f;
            }
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (currentCooldown > 0f)
            {
                return;
            }
            if (!collider.gameObject.TryGetComponent<IDamageable>(out var damagable))
            {
                return;
            }

            damagable.Damage(damage);
            WeaponHit.Invoke(this);
            currentCooldown = attackCooldown;
        }

        private void HandleStatChanged()
        {
            statsHandler.TryGetAttributeValue(statSOContainer.damage, out var damage);
            this.damage = (float)damage;
        }

        public void ResetCooldown()
        {
            currentCooldown = 0f;
        }
    }
}
