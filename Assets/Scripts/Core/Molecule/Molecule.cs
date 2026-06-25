using Common;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Core
{
    public class Molecule : MonoBehaviour, IHarvestable
    {
        [SerializeField] private ParticleSystem deathEffect;
        private MoleculeSO moleculeSO;
        
        private List<MeshRenderer> childMeshes;
        private List<Color> childColors = new();

        private float amount;
        public GameEvent<float> MoleculeHarvested { get; private set; } = new();


        public bool IsDead { get; private set; }

        public void Init(MoleculeSO moleculeSO)
        {
            this.moleculeSO = moleculeSO;
            amount = moleculeSO.amount;
            childMeshes = GetComponentsInChildren<MeshRenderer>().ToList();
            foreach (var child in childMeshes)
            {
                child.material.color = RandomUtility.RandomElement(moleculeSO.moleculeColors);
                childColors.Add(child.material.color);
            }
        }

        public float GetRemainingAmount()
        {
            return amount;
        }

        public bool Harvest(float requestedValue,out float actualValue)
        {
            if (IsDead)
            {
                actualValue = -1;
                return false;
            }
            if (amount < requestedValue)
            {
                actualValue = amount;
            }
            else
            {
                actualValue = requestedValue;
            }
            amount -= requestedValue;
            if (amount <= 0f)
            {
                Kill();
            }
            MoleculeHarvested.Invoke(actualValue);
            return true;
        }

        public void Kill()
        {
            IsDead = true;
            var effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            var main = effect.main;
            main.startColor = CreateGradient(childColors);
            Destroy(effect.gameObject, main.duration);
            Destroy(gameObject);
        }

        private Gradient CreateGradient(ICollection<Color> colors)
        {
            Gradient gradient = new();

            GradientColorKey[] colorKeys = new GradientColorKey[colors.Count];

            for (int i = 0; i < colors.Count; i++)
            {
                float time = colors.Count == 1 ? 0f : (float)i / (colors.Count - 1);

                colorKeys[i] = new GradientColorKey(colors.ElementAt(i), time);
            }

            GradientAlphaKey[] alphaKeys =
            {
                new(1f, 0f),
                new(1f, 1f)
            };

            gradient.SetKeys(colorKeys, alphaKeys);

            return gradient;
        }
    }
}
