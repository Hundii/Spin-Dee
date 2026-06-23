
//This is a generated script. You should not touch it.

using Common;
using UnityEngine;
using UnityEditor;
using System.Linq;
namespace Core.Generated
{
[CreateAssetMenu(menuName = "Common/SOContainer/CurrencySO",fileName = "CurrencySOContainer")]
public partial class CurrencySOContainer : GeneratedSOContainer
{
public CurrencySO[] currencySOArray;
public CurrencySO covidPart;
public CurrencySO herpesPart;
public CurrencySO hIVPart;
public CurrencySO influenzaPart;
public CurrencySO macroPart;
public CurrencySO microPart;
public CurrencySO molecule;
public CurrencySO virusPart;
#if UNITY_EDITOR
public override void FindReferences()
{CurrencySO[] objects = Resources.LoadAll<CurrencySO>("");
currencySOArray = objects;
covidPart = objects.Where(x=>x.name == "Covid Part").First();
herpesPart = objects.Where(x=>x.name == "Herpes Part").First();
hIVPart = objects.Where(x=>x.name == "HIV Part").First();
influenzaPart = objects.Where(x=>x.name == "Influenza Part").First();
macroPart = objects.Where(x=>x.name == "Macro Part").First();
microPart = objects.Where(x=>x.name == "Micro Part").First();
molecule = objects.Where(x=>x.name == "Molecule").First();
virusPart = objects.Where(x=>x.name == "Virus Part").First();
EditorUtility.SetDirty(this);
}
#endif
}
}
