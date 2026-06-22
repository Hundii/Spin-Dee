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

        public bool Harvest(out float value)
        {
            if (IsDead)
            {
                value = -1;
                return false;
            }
            amount -= 1;
            if (amount <= 0f)
            {
                IsDead = true;
                Destroy(gameObject);
            }
            value = 1;
            MoleculeHarvested.Invoke(value);
            return true;
        }
    }
}
