using UnityEngine;

public class CameraAimTarget : MonoBehaviour
{
    public Transform player; 
    public float distanceAhead = 3f; 
    public float heightOffset = 1f; 

    void Update()
    {
        if (player != null)
        {
            float direction = Mathf.Sign(player.localScale.x); 

            Vector3 targetPosition = player.position + 
                                    new Vector3(direction * distanceAhead, heightOffset, 0);
            
            transform.position = targetPosition;
        }
    }
}