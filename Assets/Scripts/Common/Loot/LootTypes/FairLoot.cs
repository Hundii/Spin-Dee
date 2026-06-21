using System;
using System.Collections.Generic;

namespace Common
{
    [Serializable]
    public class FairLoot : ILootDropHandler
    {
        public LootDrop drop;
        public float minAmount;
        public float maxAmount;
        protected LuckStrategy luckStrategy;
        protected Dictionary<LuckStrategy, float> luckValues;

        private bool HasFlag(LuckStrategy luckStrategy)
        {
            return (this.luckStrategy & luckStrategy) != 0;
        }

        private bool TryGetLuckValue(LuckStrategy luckStrategy, out float luckValue)
        {
            if (HasFlag(luckStrategy))
            {
                return luckValues.TryGetValue(luckStrategy, out luckValue);
            }
            luckValue = 0;
            return false;
        }
        public float GetAmount()
        {
            return UnityEngine.Random.Range(minAmount, maxAmount);
        }
        public float GetLuckyAmount()
        {
            float min = minAmount;
            float max = maxAmount;
            if (TryGetLuckValue(LuckStrategy.AddReward, out var add))
            {
                min += add;
                max += add;
            }
            if (TryGetLuckValue(LuckStrategy.MultiplyReward, out var multiply))
            {
                min *= 1 + (multiply / 100);
                max *= 1 + (multiply / 100);
            }
            if (TryGetLuckValue(LuckStrategy.Overflow, out var _))
            {
                float chance = (float)GetLuckyChance();
                if (chance > 1)
                {
                    min *= chance;
                    max *= chance;
                }
            }
            return UnityEngine.Random.Range(min, max);
        }
        public virtual decimal GetChance()
        {
            return 1m;
        }

        public LootDrop GetLoot()
        {
            return drop;
        }
        public void RegisterLuckStrategy(LuckStrategy luckStrategy)
        {
            this.luckStrategy = luckStrategy;
        }
        public void RegisterLuckValues(Dictionary<LuckStrategy, float> luckValues)
        {
            this.luckValues = luckValues;
        }

        public decimal GetLuckyChance()
        {
            var chance = GetChance();

            if (TryGetLuckValue(LuckStrategy.AddChance, out float additive))
            {
                chance += (decimal)additive;
            }
            if (TryGetLuckValue(LuckStrategy.MultiplyChance, out float multiply))
            {
                chance *= (decimal)(1 + multiply / 100);
            }
            if (TryGetLuckValue(LuckStrategy.Overflow, out float overflow))
            {
                chance *= (decimal)(1 + overflow / 100);
            }

            return chance;
        }

        public LootDropData GetLootDropData()
        {
            return new()
            {
                amount = GetAmount(),
                drop = drop
            };
        }
    }
}
