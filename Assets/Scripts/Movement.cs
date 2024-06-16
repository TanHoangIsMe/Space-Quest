using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.Translate(transform.position.x, 1f*Time.deltaTime, transform.position.z);
        }else if (Input.GetKeyUp(KeyCode.A))
        {
            Debug.Log("Left");
        }else if (Input.GetKeyDown(KeyCode.D))
        {
            transform.Translate(1f*Time.deltaTime, transform.position.y, transform.position.z);
        }
    }
}
