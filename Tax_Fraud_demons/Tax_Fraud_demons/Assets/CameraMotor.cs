using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float damp;
    Vector2 transformVector;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transformVector = Vector2.Lerp(transform.position, target.position, damp);
        transform.position = new Vector3(transformVector.x, transformVector.y, transform.position.z);
    }
}
