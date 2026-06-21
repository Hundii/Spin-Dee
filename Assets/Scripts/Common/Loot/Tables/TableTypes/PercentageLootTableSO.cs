using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    [CreateAssetMenu(menuName = "Common/LootTable/Percentage")]
    public class PercentageLootTableSO : LootTableSO
    {
        public List<PercentageLoot> dropHandlers = new();
        private PercentageIndependentLootTable _lootTable;
        public PercentageIndependentLootTable LootTable
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
