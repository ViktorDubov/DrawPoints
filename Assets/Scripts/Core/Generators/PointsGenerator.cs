using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Cysharp.Threading.Tasks;

using Scripts.Core.EventBus;
using Scripts.Extention;

namespace Scripts.Core.Generators
{
    public class PointsGenerator : MonoBehaviour
    {
        [SerializeField]
        private AssetReference pointRef;
        
        [SerializeField]
        private List<GameObject> points;

        [SerializeField]
        private GameObject[] aps_GO;

        [SerializeField]
        private BoxCollider[] colliders;

        [SerializeField]
        private WeakSubscription<GeneratePointsMessage> generateSubscription;
        [SerializeField]
        private WeakSubscription<ClearPointsMessage> clearPointsSubscription;

        public void Awake()
        {
            points = new List<GameObject>();
            aps_GO = GetApsGO();
            colliders = GetColiliderForPoints(aps_GO);

            generateSubscription = new WeakSubscription<GeneratePointsMessage>(GeneratePoints);
            clearPointsSubscription = new WeakSubscription<ClearPointsMessage>(ClearPoints);
        }
        public void OnDestroy()
        {
            generateSubscription.Dispose();
            clearPointsSubscription.Dispose();
        }

        private GameObject[] GetApsGO()
        {
            List<GameObject> aps = new List<GameObject>();
            List<GameObject> allGO = new List<GameObject>();
            gameObject.GetAllChildren(ref allGO);
            foreach (var go in allGO)
            {
                if (go.name.StartsWith("aps"))
                {
                    aps.Add(go);
                }
            }
            return aps.ToArray();
        }
        private BoxCollider[] GetColiliderForPoints(GameObject[] aps_GO)
        {
            List<BoxCollider> colliders = new List<BoxCollider>();

            Action<GameObject> AddCollider = (GameObject go) =>
            {
                if (go.TryGetComponent<BoxCollider>(out BoxCollider collider))
                {
                    colliders.Add(collider);
                }
            };

            foreach (var go in aps_GO)
            {
                AddCollider(go);
            }
            List<GameObject> goes = new List<GameObject>();
            for (int i = 0; i < aps_GO.Length; i++)
            {
                aps_GO[i].GetAllChildren(ref goes);
                foreach (var go in goes)
                {
                    AddCollider(go);
                }
                goes.Clear();
            }

            return colliders.ToArray();
        }

        private void GeneratePoints(GeneratePointsMessage message)
        {
            if (points.Count > 0)
            {
                ClearPoints();
            }
            foreach (var collider in colliders)
            {
                FillColider(collider, message);
            }
        }

        private void FillColider(BoxCollider collider, GeneratePointsMessage message)
        {
            float stepX = collider.size.x / (message.XCount + 1);
            float stepY = collider.size.y / (message.YCount + 1);
            float stepZ = collider.size.z / (message.ZCount + 1);

            float startX = collider.center.x - collider.size.x / 2 + stepX / 2;
            float finishX = collider.center.x + collider.size.x / 2 - stepX / 2;

            float startY = collider.center.y - collider.size.y / 2 + stepY / 2;
            float finishY = collider.center.y + collider.size.y / 2 - stepY / 2;

            float startZ = collider.center.z - collider.size.z / 2 + stepZ / 2;
            float finishZ = collider.center.z + collider.size.z / 2 - stepZ / 2;

            for (float i = startX; i < finishX; i += stepX)
            {
                for (float j = startY; j < finishY; j += stepY)
                {
                    for (float k = startZ; k < finishZ; k += stepZ)
                    {
                        CreatePointAsync(collider.transform, new Vector3(i, j, k)).Forget();
                    }
                }
            }
        }

        private async UniTask CreatePointAsync(Transform parent, Vector3 position)
        {
            GameObject go = await pointRef.InstantiateAsync(parent, false);
            go.transform.localPosition = position;
            points.Add(go);
        }
        private void ClearPoints(ClearPointsMessage message)
        {
            ClearPoints();
        }
        private void ClearPoints()
        {
            foreach (var point in points)
            {
                Destroy(point);
            }
            points.Clear();
        }
    }
}