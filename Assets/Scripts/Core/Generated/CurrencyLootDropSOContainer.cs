
//This is a generated script. You should not touch it.

using Common;
using UnityEngine;
using UnityEditor;
using System.Linq;
namespace Core.Generated
{
[CreateAssetMenu(menuName = "Common/SOContainer/CurrencyLootDrop",fileName = "CurrencyLootDropContainer")]
public partial class CurrencyLootDropSOContainer : GeneratedSOContainer
{
public CurrencyLootDrop[] currencyLootDropArray;
public CurrencyLootDrop covidPartLoot;
public CurrencyLootDrop herpesPartLoot;
public CurrencyLootDrop hIVPartLoot;
public CurrencyLootDrop influenzaPartLoot;
public CurrencyLootDrop macroPartLoot;
public CurrencyLootDrop microPartLoot;
public CurrencyLootDrop moleculeLoot;
public CurrencyLootDrop virusPartLoot;
#if UNITY_EDITOR
public override void FindReferences()
{CurrencyLootDrop[] objects = Resources.LoadAll<CurrencyLootDrop>("");
currencyLootDropArray = objects;
covidPartLoot = objects.Where(x=>x.name == "Covid Part Loot").First();
herpesPartLoot = objects.Where(x=>x.name == "Herpes Part Loot").First();
hIVPartLoot = objects.Where(x=>x.name == "HIV Part Loot").First();
influenzaPartLoot = objects.Where(x=>x.name == "Influenza Part Loot").First();
macroPartLoot = objects.Where(x=>x.name == "Macro Part Loot").First();
microPartLoot = objects.Where(x=>x.name == "Micro Part Loot").First();
moleculeLoot = objects.Where(x=>x.name == "Molecule Loot").First();
virusPartLoot = objects.Where(x=>x.name == "Virus Part Loot").First();
EditorUtility.SetDirty(this);
}
#endif
}
}
