
//This is a generated script. You should not touch it.

using Common;
using UnityEngine;
using UnityEditor;
using System.Linq;
namespace Core.Generated
{
[CreateAssetMenu(menuName = "Common/SOContainer/JarSO",fileName = "JarSOContainer")]
public partial class JarSOContainer : GeneratedSOContainer
{
public JarSO[] jarSOArray;
public JarSO brownJar;
public JarSO greenJar;
public JarSO purpleJar;
public JarSO redJar;
public JarSO turkoiseJar;
public JarSO whiteJar;
#if UNITY_EDITOR
public override void FindReferences()
{JarSO[] objects = Resources.LoadAll<JarSO>("");
jarSOArray = objects;
brownJar = objects.Where(x=>x.name == "Brown Jar").First();
greenJar = objects.Where(x=>x.name == "Green Jar").First();
purpleJar = objects.Where(x=>x.name == "Purple Jar").First();
redJar = objects.Where(x=>x.name == "Red Jar").First();
turkoiseJar = objects.Where(x=>x.name == "Turkoise Jar").First();
whiteJar = objects.Where(x=>x.name == "White Jar").First();
EditorUtility.SetDirty(this);
}
#endif
}
}
