using Common;
using Core.Generated;
using UnityEngine;

namespace Core
{
    public class HealthSystem
    {
        private StatSOContainer statSOContainer;
        private StatsHandler statsHandler;
        
        public float MaxHealth { get; private set; }
        public float CurrentHealth { get; private set; }

        public bool IsDead { get; private set; }

        public GameEvent<float> HealthChanged { get; private set; } = new();
        public GameEvent EntityDied { get; private set; } = new();
        public GameEvent EntityResurrected { get; private set; } = new();

        public void Init(StatsHandler statsHandler)
        {
            statSOContainer = SOContainerContainer.StatSOContainer;
            this.statsHandler = statsHandler;
            statsHandler.valueChanged += HandleStatChange;
            HandleStatChange();
            CurrentHealth = MaxHealth;
        }

        public void AddHealth(float value, bool canHealDead = false)
        {
            if (IsDead && !canHealDead)
            {
                return;
            }
            CurrentHealth += value;
            HealthChanged.Invoke(value);
            if (CurrentHealth <= 0f)
            {
                IsDead = true;
                EntityDied.Invoke();
            }
            if (IsDead && CurrentHealth > 0f)
            {
                IsDead = false;
                EntityResurrected.Invoke();
            }
        }

        private void HandleStatChange()
        {
            statsHandler.TryGetAttributeValue(statSOContainer.health, out var maxHealth);
            MaxHealth = (float)maxHealth;
        }
    }
}
