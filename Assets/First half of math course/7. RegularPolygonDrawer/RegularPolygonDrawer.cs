using UnityEngine;

namespace FG
{
    public class RegularPolygonDrawer : MonoBehaviour
    {
        [Range(3, 12)]
        public int sides = 3;
        [Range(1, 6)]
        public int density = 1;
        public float radius = 1f;
        public Color lineColor = Color.white;
        
        [Header("Vertices")]
        
        public bool drawVerts = false;
        public Color vertexColor = Color.red;
        public float vertexRadius = 0.1f;

        const float TAU = 6.283185307f;
        private Vector2 AngToDir(float angle) => new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        private float DirToAng(Vector2 dir) => Mathf.Atan2(dir.y, dir.x);
        
        private void OnDrawGizmos()
        {
            Vector2[] verts = new Vector2[sides];
            float angleBetweenVerts = TAU / sides;
            float currentAngle = angleBetweenVerts / 2f;
            Vector2 vertexDirection = AngToDir(currentAngle);

            for (int i = 0; i < verts.Length; i++)
            {
                verts[i] = vertexDirection * radius;
                currentAngle += angleBetweenVerts;
                vertexDirection = AngToDir(currentAngle);
            }

            Gizmos.color = lineColor;
            Gizmos.matrix = transform.localToWorldMatrix;
            
            for (int i = 0; i < verts.Length; i++)
            {
                Gizmos.DrawLine(verts[i], verts[(i+density) % verts.Length]);
            }
            
            if (drawVerts)
            {
                Gizmos.color = vertexColor;
                foreach (Vector2 vert in verts)
                {
                    Gizmos.DrawSphere(vert, vertexRadius);
                }
            }
        }
    }
}