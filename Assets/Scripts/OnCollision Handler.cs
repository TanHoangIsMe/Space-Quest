using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnCollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Obstacle":
                RocketCrashHandler();
                break;
            case "Finish Land":
                RocketFinishHandler();
                break;
            default:
                Debug.Log("");
                break;
        }
    }

    private void ReloadScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    private void NextLevelScene()
    {
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextScene == SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(0);
        else SceneManager.LoadScene(nextScene);
    }

    private void RocketFinishHandler()
    {
        GetComponent<Movement>().enabled = false;
        Invoke("NextLevelScene",1f);
    }

    private void RocketCrashHandler()
    {
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadScene", 1f);
    }
}