using Common;
using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(Microbe))]
    public class MicrobeReproduction : MonoBehaviour
    {
        private MicrobeSpawner microbeSpawner;
        private Harvester harvester;
        private Microbe microbe;
        private StatsHandler statsHandler;

        float currentMaterials = 0f;
        float thresholdToReproduce;
        private void Start()
        {
            microbe = GetComponent<Microbe>();
            statsHandler = microbe.GetStatsHandler();
            statsHandler.valueChanged += HandleStatChanged;
            HandleStatChanged();

            harvester = GetComponentInChildren<Harvester>();
            harvester.MaterialHarvested.RegisterListener(new(HandleMaterialHarvested));

            microbeSpawner = this.Inject<MicrobeSpawner>();
        }

        private void HandleMaterialHarvested(float amount)
        {
            currentMaterials += amount;
            if (currentMaterials >= thresholdToReproduce)
            {
                Reproduce();
                currentMaterials = 0f;
            }
        }

        private void HandleStatChanged()
        {
            statsHandler.TryGetAttributeValue(SOContainerContainer.StatSOContainer.reproductionThreshold, out var value);
            thresholdToReproduce = (float)value;
        }

        public void Reproduce()
        {
            microbeSpawner.SpawnMicrobe(transform.position,microbe.GetMicrobeSO().prefab);
        }
    }
}
