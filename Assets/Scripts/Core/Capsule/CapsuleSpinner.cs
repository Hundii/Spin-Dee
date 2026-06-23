using Common;
using Core.Generated;
using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Capsule))]
    public class CapsuleSpinner : MonoBehaviour
    {
        private float rotationSpeed;
        private Rigidbody rb;

        private StatSOContainer statSOContainer;
        private StatsHandler statsHandler;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            statsHandler = GetComponent<Capsule>().StatsHandler;
            statsHandler.valueChanged += HandleStatChange;
            statSOContainer = SOContainerContainer.StatSOContainer;
            HandleStatChange();
        }

        void FixedUpdate()
        {
            rb.MoveRotation(Quaternion.AngleAxis(rotationSpeed * Time.fixedDeltaTime, Vector3.up) * rb.rotation);
        }

        private void HandleStatChange()
        {
            statsHandler.TryGetAttributeValue(statSOContainer.spinSpeed, out var spinSpeed);
            rotationSpeed = (float)spinSpeed;
        }
    }
}
