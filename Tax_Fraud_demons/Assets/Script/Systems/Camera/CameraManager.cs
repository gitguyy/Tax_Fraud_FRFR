using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public CinemachineVirtualCamera room1Camera;
    public CinemachineVirtualCamera room2Camera;
    public CinemachineVirtualCamera room3Camera;

    public GameObject room1GameObject;
    public GameObject room2GameObject;
    public GameObject room3GameObject;

    private CinemachineVirtualCamera _currentCamera;
    private GameObject _currentGameObject;
    private bool _isSwitching;

    private void Start()
    {
        // Initialize by activating only the first room's camera and its associated GameObject
        _currentCamera = room1Camera;
        _currentGameObject = room1GameObject;

        _currentCamera.enabled = true;
        if (_currentGameObject != null)
        {
            _currentGameObject.SetActive(true);
        }
    }

    public void ActivateCamera(CinemachineVirtualCamera cameraToActivate, GameObject gameObjectToActivate = null)
    {
        if (_isSwitching || _currentCamera == cameraToActivate) return;

        StartCoroutine(SwitchCameraCoroutine(cameraToActivate, gameObjectToActivate));
    }

    private IEnumerator SwitchCameraCoroutine(CinemachineVirtualCamera cameraToActivate, GameObject gameObjectToActivate)
    {
        _isSwitching = true;

        if (_currentCamera != null)
        {
            _currentCamera.enabled = false;
            if (_currentGameObject != null)
            {
                _currentGameObject.SetActive(false);
            }
        }

        _currentCamera = cameraToActivate;
        _currentCamera.enabled = true;

        if (gameObjectToActivate != null)
        {
            _currentGameObject = gameObjectToActivate;
            _currentGameObject.SetActive(true);
        }
        else
        {
            _currentGameObject = null;
        }

        yield return new WaitForSeconds(0.1f); // Delay to prevent spamming

        _isSwitching = false;
    }
}