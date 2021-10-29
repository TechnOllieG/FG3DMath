using UnityEngine;

namespace FG
{
    public class AdaptiveFOV : MonoBehaviour
    {
        public PointGizmos[] points;
        public Camera thisCamera;

        private void OnDrawGizmos()
        {
            const float TAU = 6.283185307f;
            
            Transform tf = transform;
            Vector3 pos = tf.position;
            Matrix4x4 worldToCamera = tf.worldToLocalMatrix;

            float lowestDot = float.MaxValue;
            PointGizmos outerPoint = points[0];
            Vector3 outerPointVector = Vector3.zero;
            
            foreach (PointGizmos point in points)
            {
                Vector3 vectorToPoint = (worldToCamera.MultiplyPoint3x4(point.transform.position)).normalized;
                float dot = Vector3.Dot(Vector3.forward, vectorToPoint);
                
                if (dot < lowestDot)
                {
                    lowestDot = dot;
                    outerPoint = point;
                    outerPointVector = vectorToPoint;
                }
            }

            Transform outerTf = outerPoint.transform;
            Vector3 outerPosLocal = worldToCamera.MultiplyPoint3x4(outerTf.position);
            float fovAngleRad = Mathf.Atan((Mathf.Abs(outerPosLocal.y) + outerPoint.pointRadius) / Mathf.Abs(outerPosLocal.z));

            fovAngleRad += Mathf.Asin(outerPoint.pointRadius / Vector3.Distance(Vector3.zero, outerPosLocal));

            thisCamera.fieldOfView = fovAngleRad * Mathf.Rad2Deg * 2f;
        }
    }
}