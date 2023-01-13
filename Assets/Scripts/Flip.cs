using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flip : MonoBehaviour
{


    private float scale;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 direction = transform.localScale;
        scale = direction.x;
       
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = transform.localScale;
        //scale = direction.x;
        if (Input.GetKey("a") && !Input.GetKey("d"))
        {
            direction.x = scale * -1f;

           
        }
        if (Input.GetKey("d") && !Input.GetKey("a"))
        {
            direction.x = scale;
        }
        transform.localScale = direction;
    }
}
