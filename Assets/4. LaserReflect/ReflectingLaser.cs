using UnityEngine;

namespace FG
{
    public class ReflectingLaser : MonoBehaviour
    {
        public bool enableLaser = true;
        public int maxLaserReflections = 10;
        public float maxLaserDistance = 200f;
        
        private void OnDrawGizmos()
        {
            if (enableLaser)
            {
                Transform tf = transform;
                Vector3 origin = tf.position;
                Vector3 direction = tf.forward;

                for (int i = 1; i < maxLaserReflections; i++)
                {
                    if (!DrawLaser(origin, direction, out RaycastHit hitInfo))
                    {
                        break;
                    }
                    
                    origin = hitInfo.point;

                    Vector3 normal = hitInfo.normal;
                    Vector3 tangentVector = Vector3.Cross(normal, direction).normalized;
                    Vector3 biTangentVector = Vector3.Cross(tangentVector, normal);

                    Vector3 directionInTangentSpace;

                    // Transforms current direction to tangent space
                    directionInTangentSpace.x = Vector3.Dot(direction, tangentVector);
                    directionInTangentSpace.y = Vector3.Dot(direction, normal);
                    directionInTangentSpace.z = Vector3.Dot(direction, biTangentVector);
                    
                    // Flips the y value of the direction since the ray should bounce of the surface.
                    directionInTangentSpace.y = -directionInTangentSpace.y;
                    
                    
                }
            }
        }

        private bool DrawLaser(Vector3 origin, Vector3 direction, out RaycastHit hitInfo)
        {
            bool hit = Physics.Raycast(origin, direction, out hitInfo);
            float laserDistance;

            if (hit)
            {
                Vector3 point = hitInfo.point;
                laserDistance = Mathf.Sqrt(point.x * point.x + point.y + point.y);
            }
            else
            {
                laserDistance = maxLaserDistance;
            }
            
            Gizmos.color = Color.red;
            Gizmos.DrawLine(origin, origin + direction * laserDistance);
            Gizmos.color = Color.white;

            return hit;
        }
    }
}