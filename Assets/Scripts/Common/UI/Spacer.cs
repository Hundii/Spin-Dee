using UnityEngine;
using UnityEngine.UI;

namespace Common.UI
{
    [RequireComponent(typeof(Image))]
    public class Spacer : MonoBehaviour
    {
        private void Awake()
        {
            HideSpacer();
        }

        public void HideSpacer()
        {
            var image = GetComponent<Image>();
            image.raycastTarget = false;
            var imageColor = image.color;
            imageColor.a = 0;
            image.color = imageColor;
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        public void ShowSpacer()
        {
            var image = GetComponent<Image>();
            var imageColor = image.color;
            imageColor.a = 0.5f;
            image.color = imageColor;
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
}
