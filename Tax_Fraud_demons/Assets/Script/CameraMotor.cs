using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float damp = 0.1f; // Ensure damp is between 0 and 1 for proper interpolation
    [SerializeField]
    private Vector3 offSet;

    [SerializeField]
    private Transform leftBoundary; // Transform for the left boundary
    [SerializeField]
    private Transform rightBoundary; // Transform for the right boundary

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = target.position + offSet;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, damp);

        // Clamp the camera's position within the left and right limits using the boundary transforms
        float clampedX = Mathf.Clamp(smoothedPosition.x, leftBoundary.position.x, rightBoundary.position.x);
        transform.position = new Vector3(clampedX, smoothedPosition.y, transform.position.z);
    }
}
