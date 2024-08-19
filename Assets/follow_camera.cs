using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow_camera : MonoBehaviour
{
    public Transform target; 
    public Vector3 offset; 
    public float smoothSpeed = 5.0f;
    public float followDistanceThreshold = 5.0f; 

    private Vector3 velocity = Vector3.zero;


    private void Awake()
    {


        followDistanceThreshold = 0.1f;

    }

    private void LateUpdate()
    {
        FollowTarget();
    }

    void FollowTarget()
    {
 
        Vector3 targetPosition = target.position + offset;


        float distance = Vector3.Distance(transform.position, targetPosition);


        if (distance > followDistanceThreshold)
        {
    
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothSpeed * Time.deltaTime);
        }
    }
}
