using UnityEditor;
using UnityEngine;

public class LookAtTrigger : MonoBehaviour
{
    public Transform player;
    [Range(0f, 0.99f)]
    public float preciseness;

    private void OnDrawGizmos()
    {
        Vector2 lookDirection = player.right;
        Vector2 directionToTarget = (transform.position - player.position).normalized;
        float currentPreciseness = Vector2.Dot(lookDirection, directionToTarget);

        if (currentPreciseness > preciseness)
        {
            Handles.color = Color.green;
        }
        else
        {
            Handles.color = Color.red;
        }
        
        Handles.DrawLine(player.position, player.position + (Vector3)lookDirection);
    }
}
