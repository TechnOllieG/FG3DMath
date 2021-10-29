using UnityEngine;

namespace FG
{
    public class LocalToWorld : MonoBehaviour
    {
        public Transform objToApplyWorldSpace;
        public Vector2 localPos = Vector2.zero;
        public Vector2 worldPos;

        private void OnDrawGizmos()
        {
            worldPos = SpaceTransform.LocalToWorld(localPos, transform);
            objToApplyWorldSpace.position = worldPos;
        }
    }
}
