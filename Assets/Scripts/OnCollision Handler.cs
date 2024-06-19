using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnCollisionHandler : MonoBehaviour
{
    [SerializeField] AudioClip rocketExplosionAudio;
    [SerializeField] AudioClip finishAudio;
    [SerializeField] ParticleSystem explosionParticle;
    [SerializeField] ParticleSystem finishParticle;

    AudioSource audioSource;

    bool isTransitioning = false;
    bool isCollisionDisable = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        Move2NextLevelCheat();
        CancelCollisionCheat();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning == true || isCollisionDisable == true) return;
        else
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
        isTransitioning = true;
        finishParticle.Play();
        audioSource.PlayOneShot(finishAudio);
        GetComponent<Movement>().enabled = false;
        Invoke("NextLevelScene", 2f);
    }

    private void RocketCrashHandler()
    {
        isTransitioning = true;
        explosionParticle.Play();
        audioSource.PlayOneShot(rocketExplosionAudio);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadScene", 2f);
    }

    private void Move2NextLevelCheat()
    {
        if (Input.GetKey(KeyCode.L))
        {
            NextLevelScene();
        }
    }

    private void CancelCollisionCheat()
    {
        if (Input.GetKey(KeyCode.C))
        {
            isCollisionDisable = !isCollisionDisable;
        }
    }
}
