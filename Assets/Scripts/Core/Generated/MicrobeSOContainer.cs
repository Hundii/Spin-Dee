
//This is a generated script. You should not touch it.

using Common;
using UnityEngine;
using UnityEditor;
using System.Linq;
namespace Core.Generated
{
[CreateAssetMenu(menuName = "Common/SOContainer/MicrobeSO",fileName = "MicrobeSOContainer")]
public partial class MicrobeSOContainer : GeneratedSOContainer
{
public MicrobeSO[] microbeSOArray;
public MicrobeSO covid;
public MicrobeSO generic;
public MicrobeSO herpes;
public MicrobeSO hIV;
public MicrobeSO influenza;
#if UNITY_EDITOR
public override void FindReferences()
{MicrobeSO[] objects = Resources.LoadAll<MicrobeSO>("");
microbeSOArray = objects;
covid = objects.Where(x=>x.name == "Covid").First();
generic = objects.Where(x=>x.name == "Generic").First();
herpes = objects.Where(x=>x.name == "Herpes").First();
hIV = objects.Where(x=>x.name == "HIV").First();
influenza = objects.Where(x=>x.name == "Influenza").First();
EditorUtility.SetDirty(this);
}
#endif
}
}
