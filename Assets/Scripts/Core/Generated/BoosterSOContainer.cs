
//This is a generated script. You should not touch it.

using Common;
using UnityEngine;
using UnityEditor;
using System.Linq;
namespace Core.Generated
{
[CreateAssetMenu(menuName = "Common/SOContainer/BoosterSO",fileName = "BoosterSOContainer")]
public partial class BoosterSOContainer : GeneratedSOContainer
{
public BoosterSO[] boosterSOArray;
public BoosterSO baseBooster;
public BoosterSO capsuleDamageTier1;
public BoosterSO capsuleDamageTier2;
public BoosterSO capsuleDamageTier3;
public BoosterSO capsuleDamageTier4;
public BoosterSO capsuleMoveSpeedTier1;
public BoosterSO capsuleMoveSpeedTier2;
public BoosterSO capsuleMoveSpeedTier3;
public BoosterSO capsuleMoveSpeedTier4;
public BoosterSO capsuleSpinSpeedTier1;
public BoosterSO capsuleSpinSpeedTier2;
public BoosterSO capsuleSpinSpeedTier3;
public BoosterSO capsuleSpinSpeedTier4;
public BoosterSO moleculeHarvestAmountTier1;
public BoosterSO moleculeHarvestAmountTier2;
public BoosterSO moleculeHarvestAmountTier3;
public BoosterSO moleculeHarvestAmountTier4;
public BoosterSO moleculeHarvestSpeedTier1;
public BoosterSO moleculeHarvestSpeedTier2;
public BoosterSO moleculeHarvestSpeedTier3;
public BoosterSO moleculeHarvestSpeedTier4;
public BoosterSO scoreTier1;
public BoosterSO scoreTier2;
public BoosterSO scoreTier3;
public BoosterSO scoreTier4;
#if UNITY_EDITOR
public override void FindReferences()
{BoosterSO[] objects = Resources.LoadAll<BoosterSO>("");
boosterSOArray = objects;
baseBooster = objects.Where(x=>x.name == "Base Booster").First();
capsuleDamageTier1 = objects.Where(x=>x.name == "Capsule Damage Tier1").First();
capsuleDamageTier2 = objects.Where(x=>x.name == "Capsule Damage Tier2").First();
capsuleDamageTier3 = objects.Where(x=>x.name == "Capsule Damage Tier3").First();
capsuleDamageTier4 = objects.Where(x=>x.name == "Capsule Damage Tier4").First();
capsuleMoveSpeedTier1 = objects.Where(x=>x.name == "Capsule Move Speed Tier1").First();
capsuleMoveSpeedTier2 = objects.Where(x=>x.name == "Capsule Move Speed Tier2").First();
capsuleMoveSpeedTier3 = objects.Where(x=>x.name == "Capsule Move Speed Tier3").First();
capsuleMoveSpeedTier4 = objects.Where(x=>x.name == "Capsule Move Speed Tier4").First();
capsuleSpinSpeedTier1 = objects.Where(x=>x.name == "Capsule Spin Speed Tier1").First();
capsuleSpinSpeedTier2 = objects.Where(x=>x.name == "Capsule Spin Speed Tier2").First();
capsuleSpinSpeedTier3 = objects.Where(x=>x.name == "Capsule Spin Speed Tier3").First();
capsuleSpinSpeedTier4 = objects.Where(x=>x.name == "Capsule Spin Speed Tier4").First();
moleculeHarvestAmountTier1 = objects.Where(x=>x.name == "Molecule Harvest Amount Tier1").First();
moleculeHarvestAmountTier2 = objects.Where(x=>x.name == "Molecule Harvest Amount Tier2").First();
moleculeHarvestAmountTier3 = objects.Where(x=>x.name == "Molecule Harvest Amount Tier3").First();
moleculeHarvestAmountTier4 = objects.Where(x=>x.name == "Molecule Harvest Amount Tier4").First();
moleculeHarvestSpeedTier1 = objects.Where(x=>x.name == "Molecule Harvest Speed Tier1").First();
moleculeHarvestSpeedTier2 = objects.Where(x=>x.name == "Molecule Harvest Speed Tier2").First();
moleculeHarvestSpeedTier3 = objects.Where(x=>x.name == "Molecule Harvest Speed Tier3").First();
moleculeHarvestSpeedTier4 = objects.Where(x=>x.name == "Molecule Harvest Speed Tier4").First();
scoreTier1 = objects.Where(x=>x.name == "Score Tier1").First();
scoreTier2 = objects.Where(x=>x.name == "Score Tier2").First();
scoreTier3 = objects.Where(x=>x.name == "Score Tier3").First();
scoreTier4 = objects.Where(x=>x.name == "Score Tier4").First();
EditorUtility.SetDirty(this);
}
#endif
}
}
