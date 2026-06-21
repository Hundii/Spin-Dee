using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class MoleculeAnimator : MonoBehaviour
    {
        [SerializeField] private float positionStrength = 0.03f;
        [SerializeField] private float rotationStrength = 2f;
        [SerializeField] private int vibrato = 5;
        [SerializeField] private float randomness = 30f;

        private void Start()
        {
            foreach (Transform molecule in transform)
            {
                molecule.DOShakePosition(
                    duration: 5,
                    strength: positionStrength,
                    vibrato: vibrato,
                    randomness: randomness,
                    fadeOut: false
                ).SetLoops(-1,LoopType.Restart);

                molecule.DOShakeRotation(
                    duration: 5,
                    strength: rotationStrength,
                    vibrato: vibrato,
                    randomness: randomness,
                    fadeOut: false
                ).SetLoops(-1, LoopType.Restart);
            }
        }

        private void OnDestroy()
        {
            foreach (Transform molecule in transform)
            {
                molecule.DOKill();
            }
        }
    }
}
