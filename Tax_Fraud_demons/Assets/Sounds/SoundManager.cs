using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    private AudioSource audioSource;

    public AudioClip suitcaseSound;
    public AudioClip buttonSound;
    public AudioClip itemSlotSound;
    public AudioClip interrogationMusic;
    public AudioClip investigationScene;
    public AudioClip loseMusic;
    public AudioClip winMusic;

    private float suitcaseVolume = 0.3f;
    private float buttonVolume = 0.3f;
    private float itemSlotVolume = 0.3f;
    

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void StopMusic()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }

    public void SetSuitcaseVolume(float volume)
    {
        suitcaseVolume = volume;
    }

    public void SetButtonVolume(float volume)
    {
        buttonVolume = volume;
    }

    public void SetItemSlotVolume(float volume)
    {
        itemSlotVolume = volume;
    }

    public void PlaySuitcaseSound()
    {
        PlaySound(suitcaseSound, suitcaseVolume);
    }

    public void PlayButtonSound()
    {
        PlaySound(buttonSound, buttonVolume);
    }

    public void PlayItemSlotSound()
    {
        PlaySound(itemSlotSound, itemSlotVolume);
    }

    private void PlaySound(AudioClip clip, float volume)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip, volume);
        }
    }

    public void OnSceneTransition()
    {
        UnityEngine.SceneManagement.Scene currentScene = SceneManager.GetActiveScene();

        // Get the name of the active scene
        string sceneName = currentScene.name;
        if (sceneName == "Interrogation")
        {
            audioSource.clip = interrogationMusic;
            audioSource.Play();
        }
        if(sceneName == "Level_1")
        {
            audioSource.clip = investigationScene;
            audioSource.Play();
        }
        if (sceneName == "Lost")
        {
            audioSource.clip = loseMusic;
            audioSource.Play();
        }
        if (sceneName == "Won")
        {
            audioSource.clip = winMusic;
            audioSource.Play();
        }
    }
}