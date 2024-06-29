using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    public CameraManager cameraManager;
    public Cinemachine.CinemachineVirtualCamera leftCamera;
    public Cinemachine.CinemachineVirtualCamera rightCamera;
    public GameObject leftGameObject;
    public GameObject rightGameObject;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Vector2 playerPosition = other.transform.position;
            Vector2 triggerPosition = transform.position;

            if (playerPosition.x > triggerPosition.x)
            {
                cameraManager.ActivateCamera(rightCamera, rightGameObject);
            }
            else
            {
                cameraManager.ActivateCamera(leftCamera, leftGameObject);
            }
        }
    }
}