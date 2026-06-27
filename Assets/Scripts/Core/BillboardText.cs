using UnityEngine;

namespace Core
{
    public class BillboardText : MonoBehaviour
    {
        private Transform cam;
        private Vector3 localOffset;
        private MeshRenderer meshRenderer;

        private void Start()
        {
            cam = Camera.main.transform;
            localOffset = transform.localPosition;
            TryGetComponent(out meshRenderer);

            if (meshRenderer != null)
            {
                meshRenderer.sortingLayerName = "UI";
                meshRenderer.sortingOrder = 100;
            }
        }

        private void LateUpdate()
        {
            transform.position = transform.parent.position + localOffset;

            //transform.rotation = Quaternion.LookRotation(
            //    transform.position - cam.position
            //);

            //transform.Rotate(0, 180, 0);

            transform.rotation = Quaternion.Euler(90,0,0);
        }
    }
}
