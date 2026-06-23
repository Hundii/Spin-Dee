using UnityEngine;

namespace Core
{
    public class SimpleSpinner : MonoBehaviour
    {
        [SerializeField] private float spinSpeed = 60;
        void Update()
        {
            transform.Rotate(0, spinSpeed * Time.deltaTime, 0);
        }
    }
}
