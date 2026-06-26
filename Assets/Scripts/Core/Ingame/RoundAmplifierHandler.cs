using Common;
using UnityEngine;

namespace Core
{
    public class RoundAmplifierHandler : MonoBehaviour, INonPersistentManager
    {
        [SerializeField] private Amplifier strongerMicrobe;
        [SerializeField] private Amplifier microbeSpawnRate;
        [SerializeField] private Amplifier moleculeSpawnRate;

        public Amplifier CurrentStrongerMicrobe { get; private set; }
        public Amplifier CurrentMicrobeSpawnRate { get; private set; }
        public Amplifier CurrentMoleculeSpawnRate { get; private set; }

        private void OnEnable()
        {
            IngameEvents.RoundEnded += RampUpAmplifiers;
        }

        public void RampUpAmplifiers(int round)
        {
            if (round == 1)
            {
                CurrentStrongerMicrobe = new(strongerMicrobe);
                CurrentMicrobeSpawnRate = new(microbeSpawnRate);
                CurrentMoleculeSpawnRate = new(moleculeSpawnRate);
                return;
            }
            CurrentStrongerMicrobe.value += strongerMicrobe.value;
        }

        // Oh the workaround, 12 hours left
        public double GetDefaultStrongerMicrobeValue()
        {
            return strongerMicrobe.value;
        }

        private void OnDisable()
        {
            IngameEvents.RoundEnded -= RampUpAmplifiers;
        }
    }
}
