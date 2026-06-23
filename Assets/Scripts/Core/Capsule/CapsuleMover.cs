using Common;
using Core.Generated;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core
{
    [RequireComponent(typeof(Capsule))] 
    [RequireComponent(typeof(Rigidbody))]
    public class CapsuleMover : MonoBehaviour
    {
        [SerializeField] private LayerMask groundLayer;

        private float moveSpeed;

        private StatSOContainer statSOContainer;
        private StatsHandler statsHandler;
        private Rigidbody rb;

        private InputSystem_Actions inputActions;
        private Vector2 moveInput;

        private void Awake()
        {
            inputActions = new();
            rb = GetComponent<Rigidbody>();
            
        }

        private void Start()
        {
            statsHandler = GetComponent<Capsule>().StatsHandler;
            statsHandler.valueChanged += HandleStatChange;
            statSOContainer = SOContainerContainer.StatSOContainer;
            HandleStatChange();
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

        private void HandleStatChange()
        {
            statsHandler.TryGetAttributeValue(statSOContainer.moveSpeed, out var moveSpeed);
            this.moveSpeed = (float)moveSpeed;
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
