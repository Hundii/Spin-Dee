using Common;
using UnityEngine;

namespace Core
{
    public static class IngameEvents
    {
        public static GameEvent<Microbe> MicrobeKilledByPlayer { get; set; } = new();
        public static GameEvent<float> MoleculeMaterialHarvestedByPlayer { get; set; } = new();
        public static GameEvent<float> MoleculeMaterialHarvestedByMicrobe { get; set; } = new();
        public static GameEvent<float> ExperienceEarned { get; set; } = new();
        public static GameEvent<int> LeveledUp { get; set; } = new();

        public static GameEvent<float> ScoreChanged { get; set; } = new();
    }
}
