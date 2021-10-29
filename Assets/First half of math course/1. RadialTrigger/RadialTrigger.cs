using UnityEditor;
using UnityEngine;

public class RadialTrigger : MonoBehaviour
{
    public float radius = 30f;
    public Transform player;

    private void OnDrawGizmos()
    {
        float distance = Vector2.Distance(player.position, transform.position);

        if (distance > radius)
        {
            Handles.color = Color.red;
        }
        else
        {
            Handles.color = Color.green;
        }

        Handles.DrawWireDisc(transform.position, transform.forward, radius);
    }
}
