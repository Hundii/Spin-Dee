using Common;
using UnityEngine;

namespace Core
{
    public class IngameLevelHandler : MonoBehaviour, INonPersistentManager
    {
        private IngameLevelRequirementSO levelRequirementSO;

        private float experience;
        private int level;

        private void OnEnable()
        {
            IngameEvents.MoleculeMaterialHarvestedByPlayer.RegisterListener(HandleMoleculeMaterialHarvestedByPlayer);
        }

        private void Awake()
        {
            levelRequirementSO = SOContainerContainer.IngameLevelRequirementSOContainer.defaultLevelRequirement;
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
        }

        private void OnDisable()
        {
            IngameEvents.MoleculeMaterialHarvestedByPlayer.UnRegisterListener(HandleMoleculeMaterialHarvestedByPlayer);
        }
    }
}
