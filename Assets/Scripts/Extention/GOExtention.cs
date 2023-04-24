using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Extention
{
    public static class GOExtention
    {
        public static void GetAllChildrenTransform(this Transform parent, ref List<Transform> transformList)
        {
            foreach (Transform child in parent)
            {
                transformList.Add(child);
                child.GetAllChildrenTransform(ref transformList);
            }            
        }
        public static void GetAllChildren(this GameObject parent, ref List<GameObject> gameObjects)
        {
            List<Transform> transformChilds = new List<Transform>();
            parent.transform.GetAllChildrenTransform(ref transformChilds);
            foreach (var transform in transformChilds)
            {
                gameObjects.Add(transform.gameObject);
            }
        }
    }
}

