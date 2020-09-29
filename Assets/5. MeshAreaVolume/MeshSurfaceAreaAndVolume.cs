using UnityEngine;

namespace FG
{
    public class MeshSurfaceAreaAndVolume : MonoBehaviour
    {
        public Mesh meshToCalculate;
        public float surfaceArea = 0f;
        public float volume = 0f;
        
        private Vector3[] _vertexPositions;

        private void OnValidate()
        {
            surfaceArea = 0f;
            volume = 0f;
            
            int[] triangleIndex = meshToCalculate.triangles;
            Vector3[] vertexPositions = meshToCalculate.vertices;
            Vector3 center = meshToCalculate.bounds.center;

            for (int i = 0; i < triangleIndex.Length; i += 3)
            {
                // Triangles vertex positions
                Vector3 a = vertexPositions[triangleIndex[i]];
                Vector3 b = vertexPositions[triangleIndex[i + 1]];
                Vector3 c = vertexPositions[triangleIndex[i + 2]];

                Vector3 currentTriangleSurfaceVector = Vector3.Cross(b - a, c - a) / 2f;
                surfaceArea += currentTriangleSurfaceVector.magnitude;

                volume += Vector3.Dot(currentTriangleSurfaceVector, a - center) / 3f;
            }
        }
    }
}