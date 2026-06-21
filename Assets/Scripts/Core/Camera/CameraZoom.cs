using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core
{
    [RequireComponent(typeof(CinemachineCamera))]
    public class CameraZoom : MonoBehaviour
    {
        [SerializeField] private float zoomSpeed = 10f;
        [SerializeField] private float smoothSpeed = 10f;
        [SerializeField] private float minZoom = 3f;
        [SerializeField] private float maxZoom = 15f;

        private CinemachineCamera cinemaCamera;

        private float targetZoom;

        private void Awake()
        {
            cinemaCamera = GetComponent<CinemachineCamera>();
        }

        private void Start()
        {
            targetZoom = cinemaCamera.Lens.OrthographicSize;
        }

        private void Update()
        {
            float scroll = Mouse.current.scroll.y.ReadValue();

            targetZoom -= scroll * zoomSpeed * Time.deltaTime;
            targetZoom = Mathf.Clamp(targetZoom, minZoom, maxZoom);

            cinemaCamera.Lens.OrthographicSize = Mathf.Lerp(
                cinemaCamera.Lens.OrthographicSize,
                targetZoom,
                Time.deltaTime * smoothSpeed
            );
        }
    }
}
