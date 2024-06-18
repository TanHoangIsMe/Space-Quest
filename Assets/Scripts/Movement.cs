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
            rb.AddRelativeForce(Vector3.up * forcePower * Time.deltaTime);
            if(!audioSource.isPlaying) audioSource.PlayOneShot(rocketBoostAudio);
        }
        else
        {
            audioSource.Stop();
        }
    }

    private void GoRotate()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.back * forcePower * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.forward * forcePower * Time.deltaTime);
        }
    }
}
