using UnityEngine;

namespace FG
{
    public static class SpaceTransform
    {
        /// <summary>
        /// Converts a point from world space to local space given the "worldPoint" to convert and the "localSpace" transform that the point should be converted to be relative to.
        /// </summary>
        public static Vector3 WorldToLocal(Vector3 worldPoint, Transform localSpace)
        {
            
            return Vector3.zero;
        }

        /// <summary>
        /// Converts a position or vector from local space given the localPoint to convert and the "localSpace" transform that the point is relative to.
        /// </summary>
        public static Vector3 LocalToWorld(Vector3 localPoint, Transform localSpace)
        {
            Vector3 offset = localPoint.x * localSpace.right + localPoint.y * localSpace.up + localPoint.z * localSpace.forward;

            return localSpace.position + offset;
        }
    }
}