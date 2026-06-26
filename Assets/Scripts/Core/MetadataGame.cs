using System.Runtime.InteropServices;
using UnityEngine;

namespace Core
{
    public static class WebGLLocalStorage
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void MetaGameComplete(string key);
#endif

        public static void Save(string key = "hundii_dev/spin_dee")
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            MetaGameComplete(key);
#else
            Debug.Log($"[WebGL] Save: {key}");
#endif
        }
    }
}