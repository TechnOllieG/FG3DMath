using System;
using UnityEngine;

namespace FG
{
    public class PointGizmos : MonoBehaviour
    {
        [NonSerialized] public Matrix4x4 localSpaceMatrix = Matrix4x4.zero; // Optional matrix to input to instead draw the orientation display relative to another local space.

        public bool gizmoVisible = true;

        public bool pointVisible = true;
        public Color pointColor = Color.white;
        public float pointRadius = 0.1f;

        private void OnDrawGizmos()
        {
            if (localSpaceMatrix == Matrix4x4.zero && gizmoVisible)
            {
                Transform tf = transform;
                
                Gizmos.color = Color.red;
                Gizmos.DrawLine(tf.position, tf.right + tf.position);
                Gizmos.color = Color.green;
                Gizmos.DrawLine(tf.position, tf.up + tf.position);
                Gizmos.color = Color.cyan;
                Gizmos.DrawLine(tf.position, tf.forward + tf.position);
            }
            else if (gizmoVisible)
            {
                Vector3 position = localSpaceMatrix.GetColumn(3);
                
                Gizmos.color = Color.red;
                Gizmos.DrawLine(position, (Vector3)localSpaceMatrix.GetColumn(0) + position);
                Gizmos.color = Color.green;
                Gizmos.DrawLine(position, (Vector3)localSpaceMatrix.GetColumn(1) + position);
                Gizmos.color = Color.cyan;
                Gizmos.DrawLine(position, (Vector3)localSpaceMatrix.GetColumn(2) + position);
            }

            if (pointVisible)
            {
                Gizmos.matrix = localSpaceMatrix == Matrix4x4.zero ? transform.localToWorldMatrix : localSpaceMatrix;
                Gizmos.color = pointColor;
                Gizmos.DrawSphere(Vector3.zero, pointRadius);
            }
            Gizmos.color = Color.white;
        }
    }
}
