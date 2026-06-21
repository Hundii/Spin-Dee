using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public abstract class StatsBehaviour : MonoBehaviour
    {
        protected AmplifierSystem amplifierSystem;
        public Action valueChanged;

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
        public void RegisterAmplifiers(ICollection<Amplifier> amplifiers, float time)
        {
            amplifierSystem.RegisterAmplifiers(amplifiers);
            valueChanged?.Invoke();
            StartCoroutine(UnRegisterAmplifiersAfterTime(amplifiers, time));
        }

        private IEnumerator UnRegisterAmplifiersAfterTime(ICollection<Amplifier> amplifiers, float time)
        {
            yield return new WaitForSeconds(time);
            UnRegisterAmplifiers(amplifiers);
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
    }
}
