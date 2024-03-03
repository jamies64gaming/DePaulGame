using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("Scene Audio Clip")]
    public AudioClip defaultBackground;
    public AudioClip backgroundScene1;
    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        musicSource.clip = defaultBackground;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void ChangeBackgroundMusic(AudioClip newBackgroundClip)
    {
        musicSource.Stop();
        musicSource.clip = newBackgroundClip;
        musicSource.Play();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AudioClip newBackgroundClip = defaultBackground;

        // Determine the new background clip based on the loaded scene
        switch (scene.name)
        {
            case "SampleScene":
                newBackgroundClip = backgroundScene1;
                break;
           
                // Add more cases for other scenes
        }

        if (newBackgroundClip != null)
        {
            ChangeBackgroundMusic(newBackgroundClip);
        }
    }
}