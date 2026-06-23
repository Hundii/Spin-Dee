
//This is a generated script. You should not touch it.

using Common;
using UnityEngine;
using UnityEditor;
using System.Linq;
namespace Core.Generated
{
[CreateAssetMenu(menuName = "Common/SOContainer/MicrobeStatsSO",fileName = "MicrobeStatsSOContainer")]
public partial class MicrobeStatsSOContainer : GeneratedSOContainer
{
public MicrobeStatsSO[] microbeStatsSOArray;
public MicrobeStatsSO basic;
public MicrobeStatsSO covid;
public MicrobeStatsSO herpes;
public MicrobeStatsSO hIV;
public MicrobeStatsSO influenza;
#if UNITY_EDITOR
public override void FindReferences()
{MicrobeStatsSO[] objects = Resources.LoadAll<MicrobeStatsSO>("");
microbeStatsSOArray = objects;
basic = objects.Where(x=>x.name == "Basic").First();
covid = objects.Where(x=>x.name == "Covid").First();
herpes = objects.Where(x=>x.name == "Herpes").First();
hIV = objects.Where(x=>x.name == "HIV").First();
influenza = objects.Where(x=>x.name == "Influenza").First();
EditorUtility.SetDirty(this);
}
#endif
}
}
