using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class StatsHandler : IAmplifierSystemOwner
    {
        public AmplifierSystem amplifierSystem;
        public Action valueChanged;

        public StatsHandler()
        {
            
        }

        public StatsHandler(AmplifierSystem amplifierSystem)
        {
            this.amplifierSystem = amplifierSystem;
        }
        public bool TryGetAttributeValue(Stat stat, out double value)
        {
            var result = amplifierSystem.TryGetBuffedAttributeValue(stat, out value);
            return result;
        }

        public void RegisterAmplifiers(Amplifier amplifier)
        {
            amplifierSystem.RegisterAmplifiers(amplifier);
            valueChanged?.Invoke();
        }
        public void RegisterAmplifiers(ICollection<Amplifier> amplifiers)
        {
            amplifierSystem.RegisterAmplifiers(amplifiers);
            valueChanged?.Invoke();
        }
        public void RegisterAmplifiers(IEnumerable<IEnumerable<Amplifier>> amplifiers)
        {
            List<Amplifier> flatAmplifiers = new();
            foreach (var item in amplifiers)
            {
                flatAmplifiers.AddRange(item);
            }
            RegisterAmplifiers(flatAmplifiers);
        }
        public void UnRegisterAmplifiers(Amplifier amplifier)
        {
            amplifierSystem.UnRegisterAmplifiers(amplifier);
            valueChanged?.Invoke();
        }
        public void UnRegisterAmplifiers(ICollection<Amplifier> amplifiers)
        {
            amplifierSystem.UnRegisterAmplifiers(amplifiers);
            valueChanged?.Invoke();
        }

        public void SetOtherAmplifierSystem(IAmplifierSystemOwner amplifierSystemOwner)
        {
            amplifierSystem.SetOtherAmplifierSystems(new() { amplifierSystemOwner.GetAmplifierSystem() });
        }

        public void AddAmplifierSystem(IAmplifierSystemOwner amplifierSystemOwner)
        {
            amplifierSystem.AddAmplifierSystem(amplifierSystemOwner.GetAmplifierSystem());
        }

        public void RemoveAmplifierSystem(IAmplifierSystemOwner amplifierSystemOwner)
        {
            amplifierSystem.RemoveAmplifierSystem(amplifierSystemOwner.GetAmplifierSystem());
        }

        public void ClearOtherAmplifierSystems()
        {
            amplifierSystem.ClearOtherAmplifierSystems();
        }

        public AmplifierSystem GetAmplifierSystem()
        {
            return amplifierSystem;
        }
    }
}
