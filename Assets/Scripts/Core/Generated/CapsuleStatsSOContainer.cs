
//This is a generated script. You should not touch it.

using Common;
using UnityEngine;
using UnityEditor;
using System.Linq;
namespace Core.Generated
{
[CreateAssetMenu(menuName = "Common/SOContainer/CapsuleStatsSO",fileName = "CapsuleStatsSOContainer")]
public partial class CapsuleStatsSOContainer : GeneratedSOContainer
{
public CapsuleStatsSO[] capsuleStatsSOArray;
public CapsuleStatsSO basic;
public CapsuleStatsSO fast;
public CapsuleStatsSO god;
public CapsuleStatsSO golden;
#if UNITY_EDITOR
public override void FindReferences()
{CapsuleStatsSO[] objects = Resources.LoadAll<CapsuleStatsSO>("");
capsuleStatsSOArray = objects;
basic = objects.Where(x=>x.name == "Basic").First();
fast = objects.Where(x=>x.name == "Fast").First();
god = objects.Where(x=>x.name == "God").First();
golden = objects.Where(x=>x.name == "Golden").First();
EditorUtility.SetDirty(this);
}
#endif
}
}
