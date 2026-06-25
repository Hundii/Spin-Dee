using Common;
using UnityEngine;

namespace Core
{
    public class MicrobeReproduction : MonoBehaviour
    {
        private MicrobeSpawner microbeSpawner;
        private Harvester harvester;

        float currentMaterials = 0f;
        float thresholdToReproduce = 10f;
        private void Start()
        {
            harvester = GetComponentInChildren<Harvester>();
            harvester.MaterialHarvested.RegisterListener(HandleMaterialHarvested);

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

        public void Reproduce()
        {
            microbeSpawner.SpawnMicrobe(transform.position,gameObject);
            Debug.Log($"Reproduced {name}");
        }
    }
}
