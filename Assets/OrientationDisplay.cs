using System;
using UnityEditor;
using UnityEngine;

namespace FG
{
    public class OrientationDisplay : MonoBehaviour
    {
        public bool enabled = true;
        [NonSerialized] public Matrix4x4 localSpaceMatrix = Matrix4x4.zero; // Optional matrix to input to instead draw the orientation display relative to another local space.

        private void OnDrawGizmos()
        {
            if (localSpaceMatrix == Matrix4x4.zero && enabled)
            {
                Transform tf = transform;
                
                Handles.color = Color.red;
                Handles.DrawLine(tf.position, tf.right + tf.position);
                Handles.color = Color.green;
                Handles.DrawLine(tf.position, tf.up + tf.position);
                Handles.color = Color.cyan;
                Handles.DrawLine(tf.position, tf.forward + tf.position);
            }
            else if (enabled)
            {
                Vector3 position = localSpaceMatrix.GetColumn(3);
                
                Handles.color = Color.red;
                Handles.DrawLine(position, (Vector3)localSpaceMatrix.GetColumn(0) + position);
                Handles.color = Color.green;
                Handles.DrawLine(position, (Vector3)localSpaceMatrix.GetColumn(1) + position);
                Handles.color = Color.cyan;
                Handles.DrawLine(position, (Vector3)localSpaceMatrix.GetColumn(2) + position);
            }
        }
    }
}
