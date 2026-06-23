using UnityEngine;

namespace Core
{
    public interface IHarvestable
    {
        public bool Harvest(float requestedAmount ,out float actualAmount);
    }
}
