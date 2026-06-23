using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Core
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Rigidbody))]
    public class MicrobeMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 1.5f;
        [SerializeField] private LayerMask groundLayer;
        [Header("Idle")]
        [SerializeField] private float wanderRadius = 0.2f;
        [SerializeField] private float wanderPositionUpdateFrequencyPerSecond = 0.75f;
        [Header("Molecule Searching")]
        [SerializeField] private float searchFrequencyPerSecond = 10;
        [SerializeField] private float searchRadius = 8f;
        [SerializeField] private LayerMask moleculeLayer;

        public Vector3 inertia;
        public Vector3 desiredInertia;
        public Vector3 force;

        private Rigidbody rb;
        private NavMeshAgent agent;

        public Molecule targetMolecule;

        private bool isWandering = false;
        private Vector3 wanderDestination;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            agent.updatePosition = false;
            agent.updateRotation = false;
            agent.speed = moveSpeed;
            rb = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            StartCoroutine(FindClosestMoleculeDeposit());
            StartCoroutine(TickRandomWanderPosition());
        }

        private void FixedUpdate()
        {
            if (targetMolecule != null)
            {
                MoveToPosition(targetMolecule.transform.position);
                isWandering = false;
            }
            else
            {
                Wander();
            }
        }

        private void Wander()
        {
            if (isWandering && agent.remainingDistance <= 0.1f)
            {
                isWandering = false;
            }
            if (!isWandering)
            {
                wanderDestination = GetRandomWanderPoint();
            }
            MoveToPosition(wanderDestination);
            isWandering = true;
        }

        private void MoveToPosition(Vector3 position)
        {
            agent.SetDestination(position);
            agent.nextPosition = transform.position;

            Vector3 desiredVelocity = agent.desiredVelocity;
            desiredVelocity.y = 0;

            desiredInertia = desiredVelocity;

            Vector3 currentVelocity = rb.linearVelocity;
            currentVelocity.y = 0;

            inertia = currentVelocity;
            
            if (rb.linearVelocity.magnitude > moveSpeed)
            {
                return;
            }

            Vector3 force =
                (desiredVelocity - currentVelocity).normalized * moveSpeed;

            rb.AddForce(force, ForceMode.Acceleration);

        }

        private Vector3 GetRandomWanderPoint()
        {
            Vector2 insideCircle = Random.insideUnitCircle * wanderRadius;
            Vector3 sphere = new(insideCircle.x, 0, insideCircle.y);
            return transform.position + sphere;
        }

        IEnumerator FindClosestMoleculeDeposit()
        {
            var wait = new WaitForSeconds(1f/searchFrequencyPerSecond);
            for (; ; )
            {
                var colliders = Physics.OverlapSphere(transform.position, searchRadius, moleculeLayer);
                Molecule closestMolecule = null;
                float min = float.PositiveInfinity;
                foreach (var collider in colliders)
                {
                    if (!collider.TryGetComponent(out Molecule molecule))
                    {
                        continue;
                    }
                    var distance = Vector3.Distance(collider.transform.position, transform.position);
                    if (min > distance)
                    {
                        min = distance;
                        closestMolecule = molecule;
                    }
                }
                targetMolecule = closestMolecule;
                yield return wait;
            }
        }

        IEnumerator TickRandomWanderPosition()
        {
            var wait = new WaitForSeconds(1f / wanderPositionUpdateFrequencyPerSecond);
            for (; ; )
            {
                wanderDestination = GetRandomWanderPoint();
                yield return wait;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if ((groundLayer.value & (1 << collision.gameObject.layer)) != 0)
            {
                Invoke(nameof(FreezeYPosition),0.1f);
            }
        }

        private void FreezeYPosition()
        {
            rb.constraints |= RigidbodyConstraints.FreezePositionY;
        }
    }
}
