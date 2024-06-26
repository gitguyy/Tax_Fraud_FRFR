using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxForeground : MonoBehaviour
{
    private Vector3 startPos;
    public Transform player;
    public float parallaxStrength = 0.5f;

    void Start()
    {
        startPos = transform.position;
        //player = Camera.main.transform;
    }

    void Update()
    {
        // Calculate the parallax effect based on the player's position
        float parallaxX = (player.position.x - startPos.x) * parallaxStrength;
        float parallaxY = (player.position.y - startPos.y) * parallaxStrength;

        // Apply the parallax effect to the wall's position
        transform.position = new Vector3(startPos.x + parallaxX, startPos.y + parallaxY, transform.position.z);
    }
}
