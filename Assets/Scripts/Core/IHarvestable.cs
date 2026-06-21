using UnityEngine;

namespace Core
{
    public interface IHarvestable
    {
        public bool Harvest(out float value);
    }
}
