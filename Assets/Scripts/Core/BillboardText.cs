using UnityEngine;

namespace Core
{
    public class BillboardText : MonoBehaviour
    {
        private Transform cam;
        private Vector3 localOffset;

        private void Start()
        {
            cam = Camera.main.transform;
            localOffset = transform.localPosition;
        }

        private void LateUpdate()
        {
            transform.position = transform.parent.position + localOffset;

            //transform.rotation = Quaternion.LookRotation(
            //    transform.position - cam.position
            //);

            //transform.Rotate(0, 180, 0);

            transform.rotation = Quaternion.Euler(90,180,0);
        }
    }
}
