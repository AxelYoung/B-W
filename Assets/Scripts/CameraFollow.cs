using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    // The speed at which the camera follows
    public float followSpeed;
    // The target of the camera follow
    public Transform target;
    // The offset of the camera follow
    public Vector3 offset;

    // A reference velocity to pull from
    private Vector3 velocity = Vector3.zero;

    // Every physics based update
    void FixedUpdate()
    {
        // Find the most recent targe
        target = GameObject.FindGameObjectWithTag("Player").transform;
        // The vector destination is equal to a smooth damp position of cameras current position, its target postion, and the reference velocity and follow speed to dictate speed
        Vector3 destination = Vector3.SmoothDamp(transform.position, target.position, ref velocity, followSpeed);
        // The position of the camera is set to the destination found above plus the desired offset
        transform.position = destination + offset;
    }

}
