using System.Collections.Generic;

namespace Common
{
    public interface ILootTable
    {
        void AddDropToList(IReadOnlyCollection<ILootDropHandler> drops, List<ILootDropHandler> dropped);
        void SetLuckStrategy(LuckStrategy luckStrategy);
        void SetLuckValues(Dictionary<LuckStrategy, float> values);
    }
}
