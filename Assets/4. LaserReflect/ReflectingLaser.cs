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

                for (int i = -1; i < maxLaserReflections; i++)
                {
                    if (!DrawLaser(origin, direction, out RaycastHit hitInfo))
                    {
                        break;
                    }
                    
                    origin = hitInfo.point;

                    Vector3 normal = hitInfo.normal;

                    float projectedVectorOnNormal = Vector3.Dot(direction, normal);
                    direction -= (projectedVectorOnNormal * normal).normalized;
                }
            }
        }

        private bool DrawLaser(Vector3 origin, Vector3 direction, out RaycastHit hitInfo)
        {
            bool hit = Physics.Raycast(origin, direction, out hitInfo);

            if (hit)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(origin, hitInfo.point);
                Gizmos.color = Color.white;
            }
            else
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(origin, origin + direction * maxLaserDistance);
                Gizmos.color = Color.white;
            }

            return hit;
        }
    }
}