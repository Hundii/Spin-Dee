using Common;
using System.Linq;
using UnityEngine;

namespace Core
{
    public class Tester : MonoBehaviour
    {
        private void Start()
        {
            Amplifier roundAmp = new() { 
                amplifierScope = AmplifierScope.All, 
                amplifierType = AmplifierType.AdditivePercentage, 
                stat = SOContainerContainer.StatSOContainer.damage,
                stackCount = 100000,
                value = 10,
                uniqueTag = "All Damage Buff"
            };
            Amplifier roundAmp2 = new()
            {
                amplifierScope = AmplifierScope.All,
                amplifierType = AmplifierType.TruePercentage,
                stat = SOContainerContainer.StatSOContainer.damage,
                stackCount = 100000,
                value = 10,
                uniqueTag = "All Damage Buff2"
            };
            Amplifier roundAmp3 = new()
            {
                amplifierScope = AmplifierScope.All,
                amplifierType = AmplifierType.Plus,
                stat = SOContainerContainer.StatSOContainer.damage,
                stackCount = 100000,
                value = 1,
                uniqueTag = "All Damage Buff3"
            };
            StatsHandler statsHandler = new(new());
            statsHandler.RegisterAmplifiers(roundAmp);
            statsHandler.RegisterAmplifiers(roundAmp);
            statsHandler.RegisterAmplifiers(roundAmp2);
            statsHandler.RegisterAmplifiers(roundAmp2);
            statsHandler.RegisterAmplifiers(roundAmp3);
            statsHandler.RegisterAmplifiers(roundAmp3);

            StatsHandler statsHandler1 = new(new());
            statsHandler1.AddAmplifierSystem(statsHandler);
            Debug.Log(statsHandler.amplifierSystem.GetStatDatas().Values.ToArray()[0].GetDisplayString());
            statsHandler.TryGetAttributeValue(SOContainerContainer.StatSOContainer.damage, out var value);
            Debug.Log(value);
        }

        
    }
}
