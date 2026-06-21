using System.Collections.Generic;

namespace Common
{
    public class PercentageIndependentLootTable : LootTableBase
    {
        public PercentageIndependentLootTable(LuckStrategy luckStrategy) : base(luckStrategy)
        {
        }

        public List<ILootDropHandler> GetDrops(IReadOnlyCollection<ILootDropHandler> drops)
        {
            List<ILootDropHandler> dropped = new();
            foreach (var drop in drops)
            {
                drop.RegisterLuckStrategy(luckStrategy);
                drop.RegisterLuckValues(luckValues);
                var rng = GetRandomValue();
                if (rng < drop.GetLuckyChance())
                {
                    dropped.Add(drop);
                }
            }
            return dropped;
        }


        public override void AddDropToList(IReadOnlyCollection<ILootDropHandler> drops, List<ILootDropHandler> dropped)
        {
            dropped.AddRange(GetDrops(drops));
        }

    }
}
