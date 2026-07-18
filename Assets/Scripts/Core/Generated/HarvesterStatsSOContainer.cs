
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
public HarvesterStatsSO basicMicrobe;
#if UNITY_EDITOR
public override void FindReferences()
{HarvesterStatsSO[] objects = Resources.LoadAll<HarvesterStatsSO>("");
harvesterStatsSOArray = objects;
basic = objects.First(x=>x.name == "Basic");
basicMicrobe = objects.First(x=>x.name == "Basic Microbe");
EditorUtility.SetDirty(this);
}
#endif
}
}
