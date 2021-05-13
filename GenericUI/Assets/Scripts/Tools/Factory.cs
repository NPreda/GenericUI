using UnityEngine;

namespace Tools.Patterns
{
    public class Factory
    {
        // Reference to prefab.
        public GameObject prefab;

        public GameObject GetNewInstance(GameObject parent)
        {
            return GameObject.Instantiate(prefab, parent.transform);
        }
    }
}

