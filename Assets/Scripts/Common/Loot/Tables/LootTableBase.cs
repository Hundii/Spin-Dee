using System;
using System.Collections.Generic;

namespace Common
{
    public abstract class LootTableBase : ILootTable
    {
        protected Random rnd = new();
        protected LuckStrategy luckStrategy;
        protected Dictionary<LuckStrategy, float> luckValues;
        protected LootTableBase(LuckStrategy luckStrategy)
        {
            this.luckStrategy = luckStrategy;
        }
        public abstract void AddDropToList(IReadOnlyCollection<ILootDropHandler> drops, List<ILootDropHandler> dropped);

        public void SetLuckStrategy(LuckStrategy luckStrategy)
        {
            this.luckStrategy = luckStrategy;
        }
        private bool HasFlag(LuckStrategy luckStrategy)
        {
            return (this.luckStrategy & luckStrategy) != 0;
        }

        private bool TryGetLuckValue(LuckStrategy luckStrategy, out float luckValue)
        {
            if (HasFlag(luckStrategy))
            {
                if (luckValues == null)
                {
                    luckValue = 0;
                    return false;
                }
                return luckValues.TryGetValue(luckStrategy, out luckValue);
            }
            luckValue = 0;
            return false;
        }

        protected decimal GetRandomValue()
        {
            decimal max = rnd.NextDecimal();
            if (TryGetLuckValue(LuckStrategy.Reroll, out float rerollValue))
            {
                while (rerollValue > 100)
                {
                    var rng = rnd.NextDecimal();
                    max = max > rng ? max : rng;
                    rerollValue -= 100;
                }
                if (rerollValue / 100 > rnd.NextDouble())
                {
                    var rng = rnd.NextDecimal();
                    max = max > rng ? max : rng;
                }
            }
            return max;
        }

        public void SetLuckValues(Dictionary<LuckStrategy, float> values)
        {
            this.luckValues = values;
        }
    }
}
