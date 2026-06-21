using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class StatsHandler
    {
        protected AmplifierSystem amplifierSystem;
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

        public AmplifierSystem GetAmplifierSystem()
        {
            return amplifierSystem;
        }

        public void SetOtherAmplifierSystem(StatsHandler statsHandler)
        {
            amplifierSystem.SetOtherAmplifierSystems(new() { statsHandler.GetAmplifierSystem() });
        }

        public void ClearOtherAmplifierSystems()
        {
            amplifierSystem.SetOtherAmplifierSystems(new());
        }
    }
}
