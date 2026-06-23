
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
public LiquidSO basicLiquid;
#if UNITY_EDITOR
public override void FindReferences()
{LiquidSO[] objects = Resources.LoadAll<LiquidSO>("");
liquidSOArray = objects;
basicLiquid = objects.Where(x=>x.name == "Basic Liquid").First();
EditorUtility.SetDirty(this);
}
#endif
}
}
