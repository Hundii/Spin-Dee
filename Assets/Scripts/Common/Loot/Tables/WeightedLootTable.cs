using System.Collections.Generic;
using System.Linq;

namespace Common
{
    public class WeightedLootTable : LootTableBase
    {
        public WeightedLootTable(LuckStrategy luckStrategy) : base(luckStrategy)
        {
        }

        public ILootDropHandler GetDrop(IReadOnlyCollection<ILootDropHandler> drops)
        {
            if (Utility.IsNullOrEmpty(drops) || Utility.HasDefaultElement(drops))
            {
                return default;
            }
            decimal number = rnd.NextDecimal(drops.Sum(x => x.GetChance()));
            foreach (var drop in drops)
            {
                number -= drop.GetChance();
                if (number <= 0m)
                {
                    return drop;
                }
            }
            CustomLogger.LogError($"Something might have went wrong with a {nameof(WeightedLootTable)}");
            return default;
        }

        public override void AddDropToList(IReadOnlyCollection<ILootDropHandler> drops, List<ILootDropHandler> dropped)
        {
            var drop = GetDrop(drops);
            if (!drop.Equals(default))
            {
                dropped.Add(drop);
            }
        }

    }
}
