using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float LevelLoadDelay = 2f;
    [SerializeField] AudioClip succes;
    [SerializeField] AudioClip crash;

    [SerializeField] ParticleSystem succesParticles;
    [SerializeField] ParticleSystem crashParticles;

    AudioSource audioSource;

    bool isTransitioning = false;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();   
    }


    void OnCollisionEnter(Collision other)
    {
        if(isTransitioning)
        {
            return;
        }
        
        switch (other.gameObject.tag)
        {
            case "friendly":
                Debug.Log("This thing is friendly");
                break;

            case "Finish":
                StartSuccesSequence();
                break;

            case "fuel":
                Debug.Log("You picked up fuel");
                break;

            default:
                startCrashSequence();
                break;

        }   
    }

    void StartSuccesSequence()
    {
        isTransitioning = true;
        audioSource.Stop();   
        audioSource.PlayOneShot(succes); 
        succesParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", LevelLoadDelay);
    }

    void startCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crash);
        crashParticles.Play(); 
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", LevelLoadDelay); 
    }


    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(currentSceneIndex + 1);
        SceneManager.LoadScene(nextSceneIndex);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
