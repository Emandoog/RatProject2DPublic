using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Transform player;
    private BoxCollider2D ManagerBox;
    public GameObject CameraBoundaries;
    public GameObject _Spawners;

    // Start is called before the first frame update
    void Start()
    {
       
        ManagerBox = GetComponent<BoxCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        if (ManagerBox.bounds.Contains(player.position))
        {
            
            CameraBoundaries.SetActive(true);
            _Spawners.SetActive(true);

        }
        else
        {
           // GetComponent<SegmentManager>().CleanUpSegment();
            CameraBoundaries.SetActive(false);
            _Spawners.SetActive(false);


        }
    }

  
   

    
       
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player") 
        {
            GetComponent<SegmentManager>().SetUpSegment();
            CameraBoundaries.SetActive(true);
            _Spawners.SetActive(true);
        } 
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GetComponent<SegmentManager>().CleanUpSegment();
            CameraBoundaries.SetActive(false);
            _Spawners.SetActive(false);
        }
    }
}
