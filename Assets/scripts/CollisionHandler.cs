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

    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();   
    }


    void OnCollisionEnter(Collision other)
    {
        
        switch (other.gameObject.tag)
        {
            case "friendly":
                Debug.Log("This thing is frinedly");
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
        audioSource.PlayOneShot(succes); 
        // to do add particle effect upon succes
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", LevelLoadDelay);
    }

    void startCrashSequence()
    {
        audioSource.PlayOneShot(crash);
        // to do add particle effect upon crash 
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
