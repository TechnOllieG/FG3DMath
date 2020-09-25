using UnityEditor;
using UnityEngine;

namespace FG
{
    public class OrientationDisplay : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            Transform tf = transform;

            Handles.color = Color.cyan;
            Handles.DrawLine(tf.position, tf.forward + tf.position);
            Handles.color = Color.red;
            Handles.DrawLine(tf.position, tf.right + tf.position);
            Handles.color = Color.green;
            Handles.DrawLine(tf.position, tf.up + tf.position);
        }
    }
}
