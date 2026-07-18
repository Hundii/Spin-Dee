using Common;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class IngameLevelHandler : MonoBehaviour, INonPersistentManager
    {
        private IngameLevelRequirementSO levelRequirementSO;

        private BoosterSlotMachine boosterSlotMachine;

        private float _experience;
        public float Experience
        {
            get { return _experience; }
            private set
            {
                var before = _experience;
                _experience = value;
                IngameEvents.ExperienceEarned.Invoke(before - value);
                if (Experience >= GetLevelUpRequirement())
                {
                    LevelUp();
                }
            }
        }
        private int level;

        private float previousTimeScale;

        private SubscriptionList subscriptions = new();
        private void OnEnable()
        {
            subscriptions.Add(IngameEvents.MicrobeKilledByPlayer.RegisterListener(new(HandleMicrobeKilledByPlayer)));
            subscriptions.Add(IngameEvents.MoleculeMaterialHarvestedByPlayer.RegisterListener(new(HandleMoleculeMaterialHarvestedByPlayer)));
        }

        private void Awake()
        {
            levelRequirementSO = SOContainerContainer.IngameLevelRequirementSOContainer.defaultLevelRequirement;
        }

        private void Start()
        {
            boosterSlotMachine = this.Inject<BoosterSlotMachine>();
        }

        public void HandleMoleculeMaterialHarvestedByPlayer(float amount)
        {
            float experienceAmount = amount;
            Experience += experienceAmount;
        }
        public void HandleMicrobeKilledByPlayer(Microbe microbe)
        {
            float experienceAmount = microbe.GetExperiencePointsWorth();
            Experience += experienceAmount;
        }

        public int GetCurrentLevel()
        {
            return level;
        }

        public float GetLevelUpRequirement()
        {
            return levelRequirementSO.GetRequiredExperienceForLevel(level);
        }

        public float GetCurrentExperience()
        {
            return Experience;
        }

        public void LevelUp()
        {
            level++;
            IngameEvents.LeveledUp.Invoke(level);
            previousTimeScale = Time.timeScale;
            // Scroll snap uses DestroyImmediately internally, so we need to call it in a safe environment
            Invoke(nameof(OpenBoosterSlotMachine),0);
        }

        private void OpenBoosterSlotMachine()
        {
            boosterSlotMachine.Open();
            boosterSlotMachine.SelectionFinished.RegisterListener(new(HandleBoosterSelectionFinished,true));
            Time.timeScale = 0f;
        }

        public void HandleBoosterSelectionFinished()
        {
            Time.timeScale = previousTimeScale;
        }

        private void OnDisable()
        {
            subscriptions.UnsubscribeAll();
        }
    }
}
