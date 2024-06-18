using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnCollisionHandler : MonoBehaviour
{
    [SerializeField] AudioClip rocketExplosionAudio;
    [SerializeField] AudioClip finishAudio;

    AudioSource audioSource;

    bool isTransitioning = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Obstacle":
                RocketCrashHandler();
                isTransitioning = true;
                break;
            case "Finish Land":
                RocketFinishHandler();
                isTransitioning = true;
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
        audioSource.PlayOneShot(finishAudio);
        if (isTransitioning == true) audioSource.Stop();
        GetComponent<Movement>().enabled = false;
        Invoke("NextLevelScene",2f);
    }

    private void RocketCrashHandler()
    {
        audioSource.PlayOneShot(rocketExplosionAudio);
        if(isTransitioning == true) audioSource.Stop();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadScene", 2f);
    }
}
