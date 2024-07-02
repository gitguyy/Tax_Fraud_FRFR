using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterController
{
    public AudioClip[] footstepSounds; // Array to hold footstep sounds
    private AudioSource audioSource;
    
      protected override void Start()
    {
        base.Start();
        
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>(); // Add an AudioSource if not present
        }
    }

    // Update is called once per frame
    void Update()
    {
        getInput();
        Move();
        
    }

    void getInput()
    {
        direction = Input.GetAxisRaw("Horizontal");
    }
    
    // Method to play a random footstep sound
    public void PlayFootstep()
    {
        if (footstepSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, footstepSounds.Length);
            audioSource.clip = footstepSounds[randomIndex];
            audioSource.Play();
        }
    }
}
