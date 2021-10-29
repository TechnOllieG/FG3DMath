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
            Vector3 normalizedDistance = worldPoint - localSpace.position; // Returns the world space coordinates of the point if the local space transform would be positioned at the origin.

            Vector3 localPoint;
            localPoint.x = Vector3.Dot(normalizedDistance, localSpace.right);
            localPoint.y = Vector3.Dot(normalizedDistance, localSpace.up);
            localPoint.z = Vector3.Dot(normalizedDistance, localSpace.forward);
            
            return localPoint;
        }
        
        /// <summary>
        /// Converts a point from local space given the localPoint to convert and the "localSpace" transform that the point is relative to.
        /// </summary>
        public static Vector3 LocalToWorld(Vector3 localPoint, Transform localSpace)
        {
            Vector3 offset = localPoint.x * localSpace.right + localPoint.y * localSpace.up + localPoint.z * localSpace.forward;

            return localSpace.position + offset;
        }
    }
}