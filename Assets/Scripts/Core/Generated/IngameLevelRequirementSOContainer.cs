
//This is a generated script. You should not touch it.

using Common;
using UnityEngine;
using UnityEditor;
using System.Linq;
namespace Core.Generated
{
[CreateAssetMenu(menuName = "Common/SOContainer/IngameLevelRequirementSO",fileName = "IngameLevelRequirementSOContainer")]
public partial class IngameLevelRequirementSOContainer : GeneratedSOContainer
{
public IngameLevelRequirementSO[] ingameLevelRequirementSOArray;
public IngameLevelRequirementSO defaultLevelRequirement;
#if UNITY_EDITOR
public override void FindReferences()
{IngameLevelRequirementSO[] objects = Resources.LoadAll<IngameLevelRequirementSO>("");
ingameLevelRequirementSOArray = objects;
defaultLevelRequirement = objects.Where(x=>x.name == "Default Level Requirement").First();
EditorUtility.SetDirty(this);
}
#endif
}
}
