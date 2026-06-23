
//This is a generated script. You should not touch it.

using Common;
using UnityEngine;
using UnityEditor;
using System.Linq;
namespace Core.Generated
{
[CreateAssetMenu(menuName = "Common/SOContainer/HarvesterStatsSO",fileName = "HarvesterStatsSOContainer")]
public partial class HarvesterStatsSOContainer : GeneratedSOContainer
{
public HarvesterStatsSO[] harvesterStatsSOArray;
public HarvesterStatsSO basic;
#if UNITY_EDITOR
public override void FindReferences()
{HarvesterStatsSO[] objects = Resources.LoadAll<HarvesterStatsSO>("");
harvesterStatsSOArray = objects;
basic = objects.Where(x=>x.name == "Basic").First();
EditorUtility.SetDirty(this);
}
#endif
}
}
