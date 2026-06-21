using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Common
{
    public static class Utility
    {
        public static List<T> FindObjectsOfType<T>()
        {
            List<T> components = new();
            GameObject[] rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();

            foreach (var rootGameObject in rootGameObjects)
            {
                T[] childrenComponents = rootGameObject.GetComponentsInChildren<T>();
                foreach (var childInterface in childrenComponents)
                {
                    components.Add(childInterface);
                }
            }

            return components;
        }

        public static bool IsNullOrEmpty<T>(ICollection<T> collection)
        {
            if (collection == null || collection.Count == 0)
            {
                return true;
            }
            return false;
        }

        public static bool IsNullOrEmpty<T>(IReadOnlyCollection<T> collection)
        {
            if (collection == null || collection.Count == 0)
            {
                return true;
            }
            return false;
        }

        public static bool HasDefaultElement<T>(ICollection<T> collection)
        {
            foreach (var item in collection)
            {
                if (item.Equals(default))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool HasDefaultElement<T>(IReadOnlyCollection<T> collection)
        {
            foreach (var item in collection)
            {
                if (item.Equals(default))
                {
                    return true;
                }
            }
            return false;
        }

        public static string NormalizeFilePath(string filePath)
        {
            var path = filePath.Replace('\\', '/');
            var parts = path.Split('/');
            return Path.Combine(parts);
        }

        public static List<RaycastResult> GetElementsClicked(Vector2 clickPosition)
        {
            PointerEventData eventData = new(EventSystem.current)
            {
                position = clickPosition
            };

            List<RaycastResult> results = new();
            EventSystem.current.RaycastAll(eventData, results);

            return results;
        }

        public static bool IsElementClicked(Vector2 clickPosition, GameObject gameObject)
        {
            var clickedElements = GetElementsClicked(clickPosition);
            return clickedElements.Select(x => x.gameObject).FirstOrDefault(x => x.gameObject == gameObject) != null;
        }
    }
}
