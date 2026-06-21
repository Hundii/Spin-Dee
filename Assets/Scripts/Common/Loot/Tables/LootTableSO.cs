using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    /// <summary>
    /// Base definiton of a Loot Table. Defines what a "loot box" can contain as loot.
    /// "Opening" results in ILootDropHandlers
    /// </summary>
    public abstract class LootTableSO : ContainedSO
    {
        public Sprite icon;
        public LuckStrategy luckStrategy = LuckStrategy.Inherit;
        public void AddDropsToList(List<ILootDropHandler> dropped)
        {
            GetLootTable().AddDropToList(GetDropHandlers(), dropped);
        }
        public abstract IReadOnlyList<ILootDropHandler> GetDropHandlers();
        public abstract ILootTable GetLootTable();
        public void SetLuckStrategy(LuckStrategy luckStrategy)
        {
            GetLootTable().SetLuckStrategy(luckStrategy);
        }
        public void SetLuckValues(Dictionary<LuckStrategy, float> luckValues)
        {
            GetLootTable().SetLuckValues(luckValues);
        }
    }
}
