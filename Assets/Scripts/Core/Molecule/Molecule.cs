using Common;
using TMPro;
using UnityEngine;

namespace Core
{
    public class Molecule : MonoBehaviour, IHarvestable
    {
        public GameEvent<float> MoleculeHarvested { get; private set; } = new();

        private float amount;

        public bool IsDead { get; private set; }

        public void Init()
        {
            amount = 50f;
        }

        public float GetRemainingAmount()
        {
            return amount;
        }

        public bool Harvest(float requestedValue,out float actualValue)
        {
            if (IsDead)
            {
                actualValue = -1;
                return false;
            }
            if (amount < requestedValue)
            {
                actualValue = amount;
            }
            else
            {
                actualValue = requestedValue;
            }
            amount -= requestedValue;
            if (amount <= 0f)
            {
                IsDead = true;
                Destroy(gameObject);
            }
            MoleculeHarvested.Invoke(actualValue);
            return true;
        }
    }
}
