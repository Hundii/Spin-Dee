using System.Collections.Generic;
using System.Linq;

namespace Common
{
    public class FairLootTable : LootTableBase
    {
        public FairLootTable(LuckStrategy luckStrategy) : base(luckStrategy)
        {
        }

        public ILootDropHandler GetDrop(IReadOnlyCollection<ILootDropHandler> drops)
        {
            if (Utility.IsNullOrEmpty(drops))
            {
                return default;
            }
            return drops.ElementAt(rnd.Next(drops.Count));
        }

        public override void AddDropToList(IReadOnlyCollection<ILootDropHandler> drops, List<ILootDropHandler> dropped)
        {
            var drop = GetDrop(drops);
            if (drop == null || drop.Equals(default))
            {
                return;
            }
            drop.RegisterLuckStrategy(luckStrategy);
            drop.RegisterLuckValues(luckValues);
            dropped.Add(drop);
        }
    }
}
