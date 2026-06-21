using Common;
using TMPro;
using UnityEngine;

namespace Core
{
    public class Molecule : MonoBehaviour, IHarvestable
    {
        public GameEvent<float> MoleculeHarvested { get; private set; } = new();

        private float amount;

        public void Init()
        {
            amount = 50f;
        }

        public float GetRemainingAmount()
        {
            return amount;
        }

        public bool Harvest(out float value)
        {
            if (amount <= 0f)
            {
                value = -1;
                return false;
            }
            amount -= 1;
            value = 1;
            MoleculeHarvested.Invoke(value);
            return true;
        }
    }
}
