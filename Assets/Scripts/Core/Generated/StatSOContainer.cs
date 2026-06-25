
//This is a generated script. You should not touch it.

using Common;
using UnityEngine;
using UnityEditor;
using System.Linq;
namespace Core.Generated
{
[CreateAssetMenu(menuName = "Common/SOContainer/Stat",fileName = "StatContainer")]
public partial class StatSOContainer : GeneratedSOContainer
{
public Stat[] statArray;
public Stat damage;
public Stat dropRate;
public Stat harvestAmount;
public Stat harvestSpeed;
public Stat health;
public Stat moleculeDetectionRange;
public Stat moveSpeed;
public Stat reproductionModifier;
public Stat reproductionRate;
public Stat reproductionThreshold;
public Stat spinSpeed;
#if UNITY_EDITOR
public override void FindReferences()
{Stat[] objects = Resources.LoadAll<Stat>("");
statArray = objects;
damage = objects.Where(x=>x.name == "Damage").First();
dropRate = objects.Where(x=>x.name == "Drop Rate").First();
harvestAmount = objects.Where(x=>x.name == "Harvest Amount").First();
harvestSpeed = objects.Where(x=>x.name == "Harvest Speed").First();
health = objects.Where(x=>x.name == "Health").First();
moleculeDetectionRange = objects.Where(x=>x.name == "Molecule Detection Range").First();
moveSpeed = objects.Where(x=>x.name == "Move Speed").First();
reproductionModifier = objects.Where(x=>x.name == "Reproduction Modifier").First();
reproductionRate = objects.Where(x=>x.name == "Reproduction Rate").First();
reproductionThreshold = objects.Where(x=>x.name == "Reproduction Threshold").First();
spinSpeed = objects.Where(x=>x.name == "Spin Speed").First();
EditorUtility.SetDirty(this);
}
#endif
}
}
