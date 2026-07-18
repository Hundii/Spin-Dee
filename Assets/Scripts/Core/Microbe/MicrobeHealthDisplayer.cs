using Common;
using TMPro;
using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(Microbe))]
    public class MicrobeHealthDisplayer : MonoBehaviour
    {
        [SerializeField] private TextMeshPro healthText;

        private HealthSystem healthSystem;

        private void Start()
        {
            healthSystem = GetComponent<Microbe>().HealthSystem;
            healthSystem.HealthChanged.RegisterListener(new(UpdateDisplay));
            UpdateDisplay();
        }

        public void UpdateDisplay()
        {
            healthText.text = $"{healthSystem.CurrentHealth.AsRoundStr(1)} / {healthSystem.MaxHealth.AsRoundStr(1)}";
        }
    }
}
