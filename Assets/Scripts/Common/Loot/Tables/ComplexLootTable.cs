using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class ComplexLootTable : MonoBehaviour
    {
        [SerializeField] private LuckStrategy luckStrategy;
        [SerializeField] private List<LootTableSO> lootTables = new();
        [SerializeField] private List<LootTableSOList> premadeLootTablesList = new();

        private void Start()
        {
            foreach (var premadeLootTables in premadeLootTablesList)
            {
                lootTables.AddRange(premadeLootTables.lootTables);
            }
        }
        public List<ILootDropHandler> DropLoot(LootDrop nothingDrop = null)
        {
            List<ILootDropHandler> drops = new();
            foreach (var lootTable in lootTables)
            {
                if (lootTable.luckStrategy.HasFlag(LuckStrategy.Inherit))
                {
                    lootTable.SetLuckStrategy(lootTable.luckStrategy | luckStrategy);
                }
                lootTable.AddDropsToList(drops);
            }
            if (nothingDrop != null)
            {
                drops.RemoveAll(x => x.GetLoot() == nothingDrop);
            }
            return drops;
        }
    }

}
