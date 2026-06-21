using Common;
using TMPro;
using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(Molecule))]
    public class MoleculeAmountDisplayer : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TextMeshPro amountText;

        private Molecule molecule;

        private void Start()
        {
            molecule = GetComponent<Molecule>();
            molecule.MoleculeHarvested.RegisterListener(UpdateDisplay);
            UpdateDisplay();
        }

        public void UpdateDisplay()
        {
            amountText.text = molecule.GetRemainingAmount().AsRoundStr(1);
        }
    }
}
