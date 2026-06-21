using System.Collections.Generic;

namespace Common
{
    /// <summary>
    /// A handler on top of a Loot type. The handler is dropped first from the Loot Table.
    /// The handler still has to be "opened" to get the actual Loot (GetAmount and GetLoot functions). 
    /// </summary>
    public interface ILootDropHandler
    {
        public decimal GetChance();
        public float GetAmount();
        public float GetLuckyAmount();
        public LootDrop GetLoot();
        public void RegisterLuckStrategy(LuckStrategy luckStrategy);
        public void RegisterLuckValues(Dictionary<LuckStrategy, float> luckValues);
        public decimal GetLuckyChance();
        public LootDropData GetLootDropData();
    }
}
