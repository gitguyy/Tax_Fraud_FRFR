using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        if (target == null)
        {
            Debug.LogError("Target transform is not assigned in the Inspector.");
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindPlayerTarget();
        FindBoundaries();
    }

    void Update()
    {
        if (target != null)
        {
            Vector3 targetPosition = target.position + offSet;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, damp);

            // Clamp the camera's position within the left and right limits using the boundary transforms
            float clampedX = Mathf.Clamp(smoothedPosition.x, leftBoundary.position.x, rightBoundary.position.x);
            transform.position = new Vector3(clampedX, smoothedPosition.y, transform.position.z);
        }
        else
        {
            Debug.LogWarning("Target transform is missing or destroyed.");
        }
    }

    private void FindPlayerTarget()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            target = player.transform;
            Debug.Log("Player target re-assigned.");
        }
    }

    private void FindBoundaries()
    {
        leftBoundary = GameObject.Find("LeftBoundary")?.transform;
        rightBoundary = GameObject.Find("RightBoundary")?.transform;

        if (leftBoundary == null || rightBoundary == null)
        {
            Debug.LogError("Boundaries not found in the scene.");
        }
    }
}
