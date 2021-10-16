using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "friendly":
                Debug.Log("This thing is frinedly");
                break;

            case "Finish":
                Debug.Log("Congrants, you finished");
                break;

            case "fuel":
                Debug.Log("You picked up fuel");
                break;

            default:
                ReloadLevel();
                break;

        }   
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
