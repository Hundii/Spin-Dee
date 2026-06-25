
//This is a generated script. You should not touch it.

using Common;
using UnityEngine;
using UnityEditor;
using System.Linq;
namespace Core.Generated
{
[CreateAssetMenu(menuName = "Common/SOContainer/LiquidSO",fileName = "LiquidSOContainer")]
public partial class LiquidSOContainer : GeneratedSOContainer
{
public LiquidSO[] liquidSOArray;
public LiquidSO glitchedLiquid;
public LiquidSO greenLiquid;
public LiquidSO purpleLiquid;
public LiquidSO redLiquid;
public LiquidSO turkoiseLiquid;
#if UNITY_EDITOR
public override void FindReferences()
{LiquidSO[] objects = Resources.LoadAll<LiquidSO>("");
liquidSOArray = objects;
glitchedLiquid = objects.Where(x=>x.name == "Glitched Liquid").First();
greenLiquid = objects.Where(x=>x.name == "Green Liquid").First();
purpleLiquid = objects.Where(x=>x.name == "Purple Liquid").First();
redLiquid = objects.Where(x=>x.name == "Red Liquid").First();
turkoiseLiquid = objects.Where(x=>x.name == "Turkoise Liquid").First();
EditorUtility.SetDirty(this);
}
#endif
}
}
