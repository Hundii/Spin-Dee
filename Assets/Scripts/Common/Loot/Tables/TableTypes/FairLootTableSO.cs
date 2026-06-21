using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    [CreateAssetMenu(menuName = "Common/LootTable/Fair")]
    public class FairLootTableSO : LootTableSO
    {
        public List<FairLoot> dropHandlers;
        private FairLootTable _lootTable;
        public FairLootTable LootTable
        {
            get
            {
                _lootTable ??= new(luckStrategy);
                return _lootTable;
            }
            set
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
