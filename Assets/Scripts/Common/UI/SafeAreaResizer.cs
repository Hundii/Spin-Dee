using UnityEngine;
using UnityEngine.UI;

namespace Common.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class SafeAreaResizer : MonoBehaviour
    {
        private CanvasScaler canvasScaler;
        private float bottomUnits;
        private float topUnits;
        private Rect lastSafeArea;
        void Start()
        {
            canvasScaler = GetComponentInParent<CanvasScaler>();
            ApplyVerticalSafeArea();
        }

        private void Update()
        {
            if (lastSafeArea != Screen.safeArea)
            {
                ApplyVerticalSafeArea();
            }
        }


        public void ApplyVerticalSafeArea()
        {
            lastSafeArea = Screen.safeArea;
            var bottomPixels = Screen.safeArea.y;
            var topPixel = Screen.height - (Screen.safeArea.y + Screen.safeArea.height);

            var bottomRatio = bottomPixels / Screen.height;
            var topRatio = topPixel / Screen.height;

            var referenceResolution = canvasScaler.referenceResolution;
            bottomUnits = referenceResolution.y * bottomRatio;
            topUnits = referenceResolution.y * topRatio;

            var rectTransform = GetComponent<RectTransform>();
            rectTransform.offsetMin = new Vector2(rectTransform.offsetMin.x, bottomUnits);
            rectTransform.offsetMax = new Vector2(rectTransform.offsetMax.x, -topUnits);
        }
    }
}
