using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    [CreateAssetMenu(menuName = "Common/LootTable/Weighted")]
    public class WeightedLootTableSO : LootTableSO
    {
        public List<WeightedLoot> dropHandlers;
        private WeightedLootTable _lootTable;
        public WeightedLootTable LootTable
        {
            get
            {
                _lootTable ??= new(luckStrategy);
                return _lootTable;
            }
            private set
            {
                _lootTable = value;
            }
        }
        public override IReadOnlyList<ILootDropHandler> GetDropHandlers()
        {
            return dropHandlers;
        }

        public override ILootTable GetLootTable()
        {
            return LootTable;
        }
    }
}
