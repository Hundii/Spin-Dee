using Common;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core
{
    [RequireComponent(typeof(Rigidbody))]
    public class CapsuleMover : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private LayerMask groundLayer;

        private Rigidbody rb;

        private InputSystem_Actions inputActions;
        private Vector2 moveInput;

        private void Awake()
        {
            inputActions = new();
            rb = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            inputActions.Enable();
            inputActions.Player.Move.performed += HandleMove;
            inputActions.Player.Move.canceled += HandleMove;
        }

        private void FixedUpdate()
        {
            DoMove();
        }

        private void DoMove()
        {
            Vector3 movement = new(moveInput.x, 0f, moveInput.y);

            rb.MovePosition(
                rb.position +
                moveSpeed * Time.fixedDeltaTime * movement
            );
        }

        public void HandleMove(InputAction.CallbackContext context)
        {
            moveInput = context.ReadValue<Vector2>();
        }

        private void OnDisable()
        {
            inputActions.Player.Move.performed -= HandleMove;
            inputActions.Player.Move.canceled -= HandleMove;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if ((groundLayer.value & (1 << collision.gameObject.layer)) != 0)
            {
                rb.constraints |= RigidbodyConstraints.FreezePositionY;
            }
        }

    }
}
