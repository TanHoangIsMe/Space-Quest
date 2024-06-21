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
    List<GameObject> pointsList;

    private float timeToNextLevel = 2f;
    private bool isTransitioning = false;
    private bool isCollisionDisable = false;
    private bool isTimeRunning = false;
    private bool isFinish = false;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        pointsList = new List<GameObject>(GameObject.FindGameObjectsWithTag("Point")); 
    }

    private void Update()
    {
        if (isTimeRunning == true) 
        { 
            timeToNextLevel -= Time.deltaTime;
        }

        // move to next level
        pointsList = new List<GameObject>(GameObject.FindGameObjectsWithTag("Point"));
        NextLevelScene(pointsList.Count);

        // Cheat to debug
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
                case "Stake":
                    RocketCrashHandler();
                    break;
                case "Teleport Gate":
                    DoTeleport(true);
                    break;
                case "Teleport Destination":
                    DoTeleport(false);
                    break;
                case "Finish Land":
                    RocketFinishHandler(pointsList.Count);
                    break;
                default:
                    break;
            }
        }
    }

    private void ReloadScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    private void NextLevelScene(int point)
    {
        if(point == 0 && isFinish == true)
        {
            if (timeToNextLevel <= 0f)
            {
                timeToNextLevel = 2f;
                isTimeRunning = false;
                int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
                if (nextScene == SceneManager.sceneCountInBuildSettings)
                    SceneManager.LoadScene(0);
                else SceneManager.LoadScene(nextScene);
            }
        }
        else if (point != 0 && isFinish == true)
        {
            ReloadScene();
        } 
    }

    private void RocketFinishHandler(int point)
    {
        isFinish = true;
        isTransitioning = true;
        if (point == 0)
        {
            finishParticle.Play();
            isTimeRunning = true;
            audioSource.PlayOneShot(finishAudio);
        }      
        GetComponent<Movement>().enabled = false;
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
            NextLevelScene(3);
        }
    }

    private void CancelCollisionCheat()
    {
        if (Input.GetKey(KeyCode.C))
        {
            isCollisionDisable = !isCollisionDisable;
        }
    }

    private void DoTeleport(bool direction)
    {
        if (direction)
        {
            GameObject teleportDestination = GameObject.FindWithTag("Teleport Destination");
            gameObject.transform.position = teleportDestination.transform.position
                + new Vector3(3f, -2f, 0f);
        }
        else 
        {
            GameObject teleportGate = GameObject.FindWithTag("Teleport Gate");
            gameObject.transform.position = teleportGate.transform.position
                + new Vector3(-3f, 0f, 0f);
        }        
    }
}
