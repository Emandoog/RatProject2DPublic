using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private PlatformEffector2D _platform;
    public float tapTime = 0.2f;
    private bool waitTime;
    public bool playerIsStanding;
    
    public float secondTap;
    // Start is called before the first frame update
    void Start()
    {
        _platform = GetComponent<PlatformEffector2D>();
    }

    private void Update()

    { 
        
        if (Input.GetKeyDown("s") && playerIsStanding)
        {   
            //Wait for double tap
            if ((Time.time - secondTap) < tapTime) 
            {
                //Change the platform collider
                _platform.rotationalOffset = 180f;
                //Start Coroutine that waits for some time 
                StartCoroutine(Wait());
            }
            secondTap = Time.time;
        }
    }
    IEnumerator Wait() 
    {
        waitTime = false;
        yield return new WaitForSecondsRealtime(0.1f);
        waitTime = true;
       // Debug.Log("Platform Reset");

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
       
        //Check if player left the platform after set time
        if (collision.gameObject.CompareTag("Player") && waitTime)
        {
            _platform.rotationalOffset = 0f;
            Debug.Log("Platform reset");

        }
        if (collision.gameObject.CompareTag("Player"))
        {
            playerIsStanding = false;

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerIsStanding = true;

        }
    }

}
