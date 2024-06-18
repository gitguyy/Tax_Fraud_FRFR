using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simple_Floating : MonoBehaviour
{
    private int iterator;
    private Vector2 startPos;
    [SerializeField] private float magnitude;
    [SerializeField] private float speed;
    private void Start()
    {
        iterator = 0;
        startPos = transform.position;
    }

    void Update()
    {
        iterator++;
        transform.position = new Vector2(transform.position.x, startPos.y + magnitude * Mathf.Sin(iterator * speed));
    }
}
