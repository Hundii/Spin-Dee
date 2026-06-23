
//This is a generated script. You should not touch it.

using Common;
using UnityEngine;
using UnityEditor;
using System.Linq;
namespace Core.Generated
{
[CreateAssetMenu(menuName = "Common/SOContainer/ItemDefinitionSO",fileName = "ItemDefinitionSOContainer")]
public partial class ItemDefinitionSOContainer : GeneratedSOContainer
{
public ItemDefinitionSO[] itemDefinitionSOArray;
public ItemDefinitionSO covidPart;
public ItemDefinitionSO herpesPart;
public ItemDefinitionSO hIVPart;
public ItemDefinitionSO influenzaPart;
public ItemDefinitionSO macroPart;
public ItemDefinitionSO microPart;
public ItemDefinitionSO molecule;
public ItemDefinitionSO virusPart;
public ItemDefinitionSO nothing;
#if UNITY_EDITOR
public override void FindReferences()
{ItemDefinitionSO[] objects = Resources.LoadAll<ItemDefinitionSO>("");
itemDefinitionSOArray = objects;
covidPart = objects.Where(x=>x.name == "Covid Part").First();
herpesPart = objects.Where(x=>x.name == "Herpes Part").First();
hIVPart = objects.Where(x=>x.name == "HIV Part").First();
influenzaPart = objects.Where(x=>x.name == "Influenza Part").First();
macroPart = objects.Where(x=>x.name == "Macro Part").First();
microPart = objects.Where(x=>x.name == "Micro Part").First();
molecule = objects.Where(x=>x.name == "Molecule").First();
virusPart = objects.Where(x=>x.name == "Virus Part").First();
nothing = objects.Where(x=>x.name == "Nothing").First();
EditorUtility.SetDirty(this);
}
#endif
}
}
