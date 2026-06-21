using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(Rigidbody))]
    public class CapsuleSpinner : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed;
        private Rigidbody rb;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            rb.MoveRotation(Quaternion.AngleAxis(rotationSpeed * Time.fixedDeltaTime, Vector3.up) * rb.rotation);
        }
    }
}
