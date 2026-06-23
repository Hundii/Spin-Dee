using Common;
using UnityEngine;

namespace Core
{
    public static class IngameEvents
    {
        public static GameEvent<Microbe> MicrobeDied { get; set; } = new();
    }
}
