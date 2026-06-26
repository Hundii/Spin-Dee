using Common;
using UnityEngine;

namespace Core
{
    public static class IngameEvents
    {
        public static GameEvent<Microbe> MicrobeKilledByPlayer { get; set; } = new();
        public static GameEvent<float> PlayerMoleculeMaterialChanged { get; set; } = new();
        public static GameEvent<float> MoleculeMaterialHarvestedByPlayer { get; set; } = new();
        public static GameEvent<float> MoleculeMaterialHarvestedByMicrobe { get; set; } = new();
        public static GameEvent<float> ExperienceEarned { get; set; } = new();
        public static GameEvent<int> LeveledUp { get; set; } = new();
        public static GameEvent<StatBoosterSO> CapsuleBoosterGained { get; set; } = new();
        public static GameEvent<ScoreBoosterSO> ScoreBoosterGained { get; set; } = new();
        public static GameEvent<float> ScoreChanged { get; set; } = new();
        public static GameEvent<int> RoundEnded { get; set; } = new();
        public static GameEvent RoundWon { get; set; } = new();
        public static GameEvent GameWon { get; set; } = new();
        public static GameEvent RoundLost { get; set; } = new();
        public static GameEvent RoundContinuedByPlayer { get; set; } = new();
        public static GameEvent<int> RoundStarted { get; set; } = new();
    }
}
