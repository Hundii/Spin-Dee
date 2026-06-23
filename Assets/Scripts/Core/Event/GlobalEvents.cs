using Common;
using UnityEngine;

namespace Core
{
    public static class GlobalEvents
    {
        public static GameEvent<float> MoleculeMaterialHarvestedByPlayer { get; set; } = new();
        public static GameEvent<float> ExperienceEarned { get; set; } = new();
        public static GameEvent<int> IngameLevelledUp { get; set; } = new();
        public static GameEvent GameSaved { get; set; } = new();
        public static GameEvent GameLoaded { get; set; } = new();
        public static GameEvent PlayerInventorySavedToDisk { get; set; } = new();

    }
}
