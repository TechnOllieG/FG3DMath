using UnityEngine;

namespace FG
{
    public class TurretPlace : MonoBehaviour
    {
        public float maxRaycastDistance = 100f;
        public float radiusOfVertices = 0.2f;
        public Color raycastColor = Color.red;
        public Color vertexColor = Color.red;
        public Color meshEdgeColor = Color.white;
        
        public float turretHeightFromGround = 0.5f;
        public float distanceBetweenBarrels = 0.2f;
        public float barrelLength = 1f;

        private PointGizmos _pointGizmos;
        private Mesh _mainBodyMesh;
        private Mesh _gunMesh;

        private void OnDrawGizmos()
        {
            Transform playerTransform = transform;
            Vector3 lookDir = playerTransform.forward;
            Vector3 playerPos = playerTransform.position;

            Gizmos.DrawSphere(playerPos, 0.1f);
            
            if (Physics.Raycast(playerPos, lookDir, out RaycastHit hitInfo, maxRaycastDistance))
            {
                Gizmos.color = raycastColor;
                Gizmos.DrawLine(playerPos, hitInfo.point);

                Vector3 up = hitInfo.normal;
                Vector3 right = Vector3.Cross(up, lookDir).normalized;
                Vector3 forward = Vector3.Cross(right, up);

                Quaternion lookRotation = Quaternion.LookRotation(forward, up);

                Matrix4x4 turretToWorld = Matrix4x4.TRS(hitInfo.point, lookRotation, Vector3.one);
                if (_pointGizmos == isActiveAndEnabled)
                {
                    _pointGizmos.localSpaceMatrix = turretToWorld;
                    _pointGizmos.gizmoVisible = true;
                }

                Gizmos.matrix = turretToWorld;

                SetupMeshes();

                DrawVertices(_mainBodyMesh.vertices, vertexColor, radiusOfVertices);
                DrawTriangles(_mainBodyMesh.vertices, _mainBodyMesh.triangles, meshEdgeColor);
                DrawGuns(_gunMesh.vertices, _gunMesh.triangles);
            }
            else
            {
                if (_pointGizmos == isActiveAndEnabled)
                {
                    _pointGizmos.localSpaceMatrix = Matrix4x4.zero;
                    _pointGizmos.gizmoVisible = false;
                }

                Gizmos.color = raycastColor;
                Gizmos.DrawLine(playerPos, lookDir * maxRaycastDistance + playerPos);
            }
            
            Gizmos.color = Color.white;
        }

        private void OnValidate()
        {
            _pointGizmos = GetComponent<PointGizmos>();
        }

        private void SetupMeshes()
        {
            // Standard triangle when the cuboids vertices begin with the bottom 4 vertices in an anti clockwise manner,
            // the top begins above index 0 continuing in an anti clockwise manner
            int[] trianglesCuboidMesh = new int[]
            {
                // Bottom face
                0, 1, 2,
                0, 3, 2,
                // Top face
                4, 5, 6,
                4, 7, 6,
                // Right face
                0, 3, 7,
                0, 4, 7,
                // Left face
                1, 5, 6,
                1, 2, 6,
                // Front face
                2, 6, 7,
                2, 3, 7,
                // Back face
                0, 4, 5,
                0, 1, 5
            };
            
            _mainBodyMesh = new Mesh
            {
                vertices = new Vector3[]
                {
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
                }, 
                triangles = trianglesCuboidMesh
            };

            _gunMesh = new Mesh
            {
                vertices = new Vector3[]
                {
                    // bottom 4 positions:
                    new Vector3(0.125f, turretHeightFromGround, 1 + barrelLength),
                    new Vector3(-0.125f, turretHeightFromGround, 1 + barrelLength),
                    new Vector3(-0.125f, turretHeightFromGround, 1),
                    new Vector3(0.125f, turretHeightFromGround, 1),
                    // top 4 positions:
                    new Vector3(0.125f, turretHeightFromGround + 0.25f,
                        1 + barrelLength),
                    new Vector3(-0.125f, turretHeightFromGround + 0.25f,
                        1 + barrelLength),
                    new Vector3(-0.125f, turretHeightFromGround + 0.25f, 1),
                    new Vector3(0.125f, turretHeightFromGround + 0.25f, 1)
                },
                triangles = trianglesCuboidMesh
            };
        }

        private void DrawGuns(Vector3[] gunMeshVertices, int[] gunMeshTris)
        {
            for (int i = 0; i < gunMeshVertices.Length; i++)
            {
                gunMeshVertices[i].x += distanceBetweenBarrels * 0.5f;
            }
            DrawTriangles(gunMeshVertices, gunMeshTris, meshEdgeColor);
            
            for (int i = 0; i < gunMeshVertices.Length; i++)
            {
                gunMeshVertices[i].x -= distanceBetweenBarrels;
            }
            DrawTriangles(gunMeshVertices, gunMeshTris, meshEdgeColor);
        }
        
        private void DrawTriangles(Vector3[] vertices, int[] tris, Color lineColor)
        {
            for (int i = 0; i < tris.Length; i += 3)
            {
                Vector3 vert1 = vertices[tris[i]];
                Vector3 vert2 = vertices[tris[i + 1]];
                Vector3 vert3 = vertices[tris[i + 2]];

                Gizmos.color = lineColor;

                Gizmos.DrawLine(vert1, vert2);
                Gizmos.DrawLine(vert2, vert3);
                Gizmos.DrawLine(vert3, vert1);
            }
        }
        
        private void DrawVertices(Vector3[] vertices, Color color, float vertexRadius)
        {
            if (vertexRadius < 0.00001f)
            {
                return;
            }
            foreach (Vector3 vert in vertices)
            {
                Gizmos.color = color;
                Gizmos.DrawSphere(vert, vertexRadius);
            }
        }
    }
}