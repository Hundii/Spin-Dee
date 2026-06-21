using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core
{
    public class Capsule : MonoBehaviour
    {
        private List<Harvester> harvesters = new();

        private void Start()
        {
            harvesters = GetComponentsInChildren<Harvester>().ToList();

            foreach (var harvester in harvesters)
            {
                harvester.MaterialHarvested.RegisterListener(HandleHarvestedMaterial);
            }
        }

        private void HandleHarvestedMaterial(float amount)
        {
            GlobalEvents.MoleculeMaterialHarvestedByPlayer.Invoke(amount);
        }
    }
}
