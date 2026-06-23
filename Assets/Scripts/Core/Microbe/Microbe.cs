using Common;
using System.Linq;
using UnityEngine;

namespace Core
{
    public class Microbe : MonoBehaviour, IHarvesterStatsHolder, IStatsHandlerHolder, IDamageable
    {
        [Header("References")]
        [SerializeField] private MicrobeSO microbeSO;
        [Header("Effect")]
        [SerializeField] private ParticleSystem deathEffect;
        [SerializeField] private Material effectColor;

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
            var effect = Instantiate(deathEffect,transform.position,Quaternion.identity);
            var main = effect.main;
            main.startColor = effectColor.color;
            effect.Play();
            Destroy(effect, effect.main.duration);
            Destroy(gameObject);
        }
    }
}
