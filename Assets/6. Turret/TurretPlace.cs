using UnityEditor;
using UnityEngine;

namespace FG
{
    public class TurretPlace : MonoBehaviour
    {
        public float maxRaycastDistance = 100f;
        public float radiusOfVertices = 0.2f;
        public Color raycastColor = Color.red;
        public Color vertexColor = Color.red;
        
        
        private OrientationDisplay _orientationDisplay;
        
        private void OnDrawGizmos()
        {
            Transform playerTransform = transform;
            Vector3 lookDir = playerTransform.forward;
            Vector3 playerPos = playerTransform.position;

            Gizmos.DrawSphere(playerPos, 0.1f);
            
            if (Physics.Raycast(playerPos, lookDir, out RaycastHit hitInfo, maxRaycastDistance))
            {
                Handles.color = raycastColor;
                Handles.DrawLine(playerPos, hitInfo.point);

                Vector3 up = hitInfo.normal;
                Vector3 right = Vector3.Cross(up, lookDir).normalized;
                Vector3 forward = Vector3.Cross(right, up);

                Quaternion lookRotation = Quaternion.LookRotation(forward, up);

                Vector3[] corners = new Vector3[]{
                    // bottom 4 positions:
                    new Vector3( 1, 0, 1 ),
                    new Vector3( -1, 0, 1 ),
                    new Vector3( -1, 0, -1 ),
                    new Vector3( 1, 0, -1 ),
                    // top 4 positions:
                    new Vector3( 1, 2, 1 ),
                    new Vector3( -1, 2, 1 ),
                    new Vector3( -1, 2, -1 ),
                    new Vector3( 1, 2, -1 ) 
                };
                
                Matrix4x4 turretToWorld = Matrix4x4.TRS(hitInfo.point, lookRotation, Vector3.one);
                if (_orientationDisplay == isActiveAndEnabled)
                {
                    _orientationDisplay.localSpaceMatrix = turretToWorld;
                    _orientationDisplay.enabled = true;
                }

                for (int i = 0; i < corners.Length; i++)
                {
                    Vector3 worldPoint = turretToWorld.MultiplyPoint3x4(corners[i]);

                    Gizmos.color = vertexColor;
                    Gizmos.DrawSphere(worldPoint, radiusOfVertices);
                }
            }
            else
            {
                if (_orientationDisplay == isActiveAndEnabled)
                {
                    _orientationDisplay.localSpaceMatrix = Matrix4x4.zero;
                    _orientationDisplay.enabled = false;
                }

                Handles.color = raycastColor;
                Handles.DrawLine(playerPos, lookDir * maxRaycastDistance);
            }
            
            Handles.color = Color.white;
        }

        private void OnValidate()
        {
            _orientationDisplay = GetComponent<OrientationDisplay>();
        }
    }
}