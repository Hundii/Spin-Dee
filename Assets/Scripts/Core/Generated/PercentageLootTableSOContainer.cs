
//This is a generated script. You should not touch it.

using Common;
using UnityEngine;
using UnityEditor;
using System.Linq;
namespace Core.Generated
{
[CreateAssetMenu(menuName = "Common/SOContainer/PercentageLootTableSO",fileName = "PercentageLootTableSOContainer")]
public partial class PercentageLootTableSOContainer : GeneratedSOContainer
{
public PercentageLootTableSO[] percentageLootTableSOArray;
public PercentageLootTableSO covidLootTable;
public PercentageLootTableSO genericVirusLootTable;
public PercentageLootTableSO herpesLootTable;
public PercentageLootTableSO hIVLootTable;
public PercentageLootTableSO influenzaLootTable;
#if UNITY_EDITOR
public override void FindReferences()
{PercentageLootTableSO[] objects = Resources.LoadAll<PercentageLootTableSO>("");
percentageLootTableSOArray = objects;
covidLootTable = objects.Where(x=>x.name == "Covid Loot Table").First();
genericVirusLootTable = objects.Where(x=>x.name == "Generic Virus Loot Table").First();
herpesLootTable = objects.Where(x=>x.name == "Herpes Loot Table").First();
hIVLootTable = objects.Where(x=>x.name == "HIV Loot Table").First();
influenzaLootTable = objects.Where(x=>x.name == "Influenza Loot Table").First();
EditorUtility.SetDirty(this);
}
#endif
}
}
