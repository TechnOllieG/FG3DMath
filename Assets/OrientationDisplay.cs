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
            Handles.DrawLine(tf.position, tf.forward);
            Handles.color = Color.red;
            Handles.DrawLine(tf.position, tf.right);
            Handles.color = Color.green;
            Handles.DrawLine(tf.position, tf.up);
        }
    }
}
