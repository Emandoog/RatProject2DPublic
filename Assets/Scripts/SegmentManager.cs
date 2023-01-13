using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetUpSegment() 
    {

        Debug.Log("SegmentSetUP");
        //GameObject.Find("LevelOne/Spawner").GetComponent<EnemySpawner>().SpawnEnemy();
        //GameObject.FindObjectOfType()
    }
   public  void CleanUpSegment() 
    {
        Debug.Log("SegmentCleanUP");
    }
}
