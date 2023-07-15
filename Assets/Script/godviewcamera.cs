using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class godviewcamera : MonoBehaviour
{
    
    public Transform followTarget;
    public float followDistance;
    public float scrollSpeed;
    private Vector3 followDirection;
    // Start is called before the first frame update
    void Start()
    {
        followDirection = transform.position - followTarget.position;
        followDistance = followDirection.magnitude;
        Debug.Log("followDistance " + followDistance);
        followDirection.Normalize();
    }

    public void MoveCam()
    {
        Vector2 vScroll = Input.mouseScrollDelta;
        followDistance -= vScroll.y * scrollSpeed;
        if (followDistance < 4)
        {
            followDistance = 4.0f;
        }
        else if (followDistance > 20.0f)
        {
            followDistance = 20.0f;
        }

        Vector3 camPos = followTarget.position + followDirection * followDistance;
        transform.position = camPos;
    }
}
