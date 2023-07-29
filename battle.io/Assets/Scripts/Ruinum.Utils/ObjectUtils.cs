using UnityEngine;

namespace Ruinum.Utils
{
    public static class ObjectUtils
    {
        public static T CreateGameObject<T>(GameObject prefab) where T: Component
        {
            return UnityEngine.Object.Instantiate(prefab).GetComponent<T>();
        }

        public static T CreateGameObject<T>(GameObject prefab, Transform parent) where T : Component
        {
            return UnityEngine.Object.Instantiate(prefab, parent).GetComponent<T>();
        }
    }
}
