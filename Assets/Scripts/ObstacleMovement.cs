using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    [SerializeField] Vector3 movementVector;
    [SerializeField] [Range(0,1)] float speed;
    [SerializeField] float period;

    Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) { return; } // it is unsafe to compare floats. Mathf.Epsilon is smallest float number 
        const float tau = Mathf.PI * 2; // tau = 2pi = 1 circle

        float cycles = Time.time / period; // value for how many circle was create that time has pass 

        float rawSin = Mathf.Sin(tau * cycles); // get a number between -1 -> 1

        speed = (rawSin + 1) / 2; // get number form 0 -> 1

        Vector3 offset = movementVector * speed; 

        transform.position = startPosition + offset;
    }
}
