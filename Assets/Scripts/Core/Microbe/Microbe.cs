using Common;
using System.Linq;
using UnityEngine;

namespace Core
{
    public class Microbe : MonoBehaviour, IHarvesterStatsHolder, IStatsHandlerHolder, IDamageable
    {
        [SerializeField] private MicrobeSO microbeSO;
        private StatsHandler statsHandler;

        public HealthSystem HealthSystem { get; private set; }

        public GameEvent<float> MicrobeDamaged { get; private set; } = new();

        private void Awake()
        {
            statsHandler = new(new(microbeSO.microbeStats.stats.Select(x => x.stat), microbeSO.microbeStats.stats.Select(x => x.value)));
            HealthSystem = new();
            HealthSystem.Init(statsHandler);
            HealthSystem.EntityDied.RegisterListener(HandleDeath);
        }

        public HarvesterStatsSO GetHarvesterStats()
        {
            return microbeSO.microbeStats.harvesterStats;
        }

        public StatsHandler GetStatsHandler()
        {
            return statsHandler;
        }

        public void Damage(float amount)
        {
            HealthSystem.AddHealth(-amount);
            MicrobeDamaged.Invoke(amount);
        }

        public void HandleDeath()
        {
            IngameEvents.MicrobeDied.Invoke(this);
            Destroy(gameObject);
        }
    }
}
