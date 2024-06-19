using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audioSource;

    [SerializeField] float forcePower;
    [SerializeField] float rotatePower;
    [SerializeField] AudioClip rocketBoostAudio;
    [SerializeField] ParticleSystem rocketBoosterParticle;
    [SerializeField] ParticleSystem leftWingParticle;
    [SerializeField] ParticleSystem rightWingParticle;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    { 
        GoUp();
        GoRotate();
    }

    private void GoUp()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartGoUp();
        }
        else
        {
            StopGoUpEffect();
        }
    }

    private void StopGoUpEffect()
    {
        audioSource.Stop();
        rocketBoosterParticle.Stop();
    }

    private void StartGoUp()
    {
        rb.AddRelativeForce(Vector3.up * forcePower * Time.deltaTime);
        if (!audioSource.isPlaying) audioSource.PlayOneShot(rocketBoostAudio);
        if (!rocketBoosterParticle.isPlaying) rocketBoosterParticle.Play();
    }

    private void GoRotate()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            StartRotateRight();
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            StartRotateLeft();
        }
        else
        {
            StopRotateEffect();
        }
    }

    private void StopRotateEffect()
    {
        rightWingParticle.Stop();
        leftWingParticle.Stop();
    }

    private void StartRotateLeft()
    {
        transform.Rotate(Vector3.forward * forcePower * Time.deltaTime);
        if (!leftWingParticle.isPlaying) leftWingParticle.Play();
    }

    private void StartRotateRight()
    {
        transform.Rotate(Vector3.back * forcePower * Time.deltaTime);
        if (!rightWingParticle.isPlaying) rightWingParticle.Play();
    }
}
