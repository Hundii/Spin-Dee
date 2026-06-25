
//This is a generated script. You should not touch it.

using Common;
using UnityEngine;
using UnityEditor;
using System.Linq;
namespace Core.Generated
{
[CreateAssetMenu(menuName = "Common/SOContainer/CapsuleSO",fileName = "CapsuleSOContainer")]
public partial class CapsuleSOContainer : GeneratedSOContainer
{
public CapsuleSO[] capsuleSOArray;
public CapsuleSO blue;
public CapsuleSO glitch;
public CapsuleSO gold;
public CapsuleSO red;
public CapsuleSO white;
#if UNITY_EDITOR
public override void FindReferences()
{CapsuleSO[] objects = Resources.LoadAll<CapsuleSO>("");
capsuleSOArray = objects;
blue = objects.Where(x=>x.name == "Blue").First();
glitch = objects.Where(x=>x.name == "Glitch").First();
gold = objects.Where(x=>x.name == "Gold").First();
red = objects.Where(x=>x.name == "Red").First();
white = objects.Where(x=>x.name == "White").First();
EditorUtility.SetDirty(this);
}
#endif
}
}
