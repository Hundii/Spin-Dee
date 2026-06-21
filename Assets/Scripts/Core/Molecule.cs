using UnityEngine;

namespace Core
{
    public class Molecule : MonoBehaviour, IHarvestable
    {
        public float Harvest()
        {
            return 1;
        }
    }
}
