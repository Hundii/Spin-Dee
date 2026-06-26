using Common;
using NUnit.Framework;
using System.Collections.Generic;
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

        private Harvester[] harvesters;

        private void Awake()
        {
            statsHandler = new(new(microbeSO.microbeStats.stats.Select(x => x.stat), microbeSO.microbeStats.stats.Select(x => x.value)));
            HealthSystem = new();

            harvesters = GetComponentsInChildren<Harvester>();
            foreach (var harvester in harvesters)
            {
                harvester.MaterialHarvested.RegisterListener(HandleMaterialHarvested);
            }
        }

        private void Start()
        {
            var roundAmplifierHandler = this.Inject<RoundAmplifierHandler>();
            statsHandler.RegisterAmplifiers(roundAmplifierHandler.CurrentStrongerMicrobe);
            HealthSystem.Init(statsHandler);
            HealthSystem.EntityDied.RegisterListener(HandleDeath);
        }

        private void HandleMaterialHarvested(float amount)
        {
            IngameEvents.MoleculeMaterialHarvestedByMicrobe.Invoke(amount);
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
            var effect = Instantiate(deathEffect,transform.position,Quaternion.identity);
            var main = effect.main;
            main.startColor = effectColor.color;
            effect.Play();

            DropLoot();

            IngameEvents.MicrobeKilledByPlayer.Invoke(this);

            Destroy(effect.gameObject, effect.main.duration);
            Destroy(gameObject);
        }

        public void DropLoot()
        {
            var dropHandlers = microbeSO.lootTable.GetDropHandlers();
            foreach (var dropHandler in dropHandlers)
            {
                var data = dropHandler.GetLootDropData();
                data.drop.Activate(data.amount);
            }
        }
    }
}
