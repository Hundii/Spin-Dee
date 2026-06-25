using Common;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class IngameLevelHandler : MonoBehaviour, INonPersistentManager
    {
        private IngameLevelRequirementSO levelRequirementSO;

        private BoosterSlotMachine boosterSlotMachine;

        private float experience;
        private int level;

        private float previousTimeScale;

        private void OnEnable()
        {
            IngameEvents.MoleculeMaterialHarvestedByPlayer.RegisterListener(HandleMoleculeMaterialHarvestedByPlayer);
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
            float experienceAmount = 1;
            experience += experienceAmount;
            IngameEvents.ExperienceEarned.Invoke(experienceAmount);

            if (experience >= GetLevelUpRequirement())
            {
                LevelUp();
            }
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
            return experience;
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
            boosterSlotMachine.SelectionFinished.RegisterOneTimeListener(HandleBoosterSelectionFinished);
            Time.timeScale = 0f;
        }

        public void HandleBoosterSelectionFinished()
        {
            Time.timeScale = previousTimeScale;
        }

        private void OnDisable()
        {
            IngameEvents.MoleculeMaterialHarvestedByPlayer.UnRegisterListener(HandleMoleculeMaterialHarvestedByPlayer);
        }
    }
}
