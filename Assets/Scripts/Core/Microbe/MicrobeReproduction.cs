using UnityEngine;

namespace Core
{
    public class MicrobeReproduction : MonoBehaviour
    {
        private Harvester harvester;

        float currentMaterials = 0f;
        float thresholdToReproduce = 10f;
        private void Start()
        {
            harvester = GetComponentInChildren<Harvester>();
            harvester.MaterialHarvested.RegisterListener(HandleMaterialHarvested);
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
            Debug.Log($"Reproduced {name}");
        }
    }
}
