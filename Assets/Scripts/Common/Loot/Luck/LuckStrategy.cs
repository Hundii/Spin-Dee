using System;

namespace Common
{
    [Flags]
    public enum LuckStrategy
    {
        None = 0,
        Inherit = 1 << 0,
        // Reroll and choose the better chance
        Reroll = 1 << 1,
        // Multiply chance
        MultiplyChance = 1 << 2,
        // Multiply the reward
        MultiplyReward = 1 << 3,
        // Multiply chance, if it is bigger than 100%, overflow to reward
        Overflow = 1 << 4,
        // Add flat chance
        AddChance = 1 << 5,
        // Add flat reward
        AddReward = 1 << 6,
        All = Inherit | Reroll | MultiplyChance | Overflow | AddChance | MultiplyReward
    }
}
