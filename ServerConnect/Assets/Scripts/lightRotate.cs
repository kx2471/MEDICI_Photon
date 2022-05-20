using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightRotate : MonoBehaviour
{
    public float speed = 200;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
       transform.Rotate(new Vector3(0, speed * Time.deltaTime, 0));

        
            while (speed <= 0)
            {
                speed--;
                break;
            }
            
        
    }
}
