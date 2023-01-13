using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentTransition : MonoBehaviour
{
    private GameObject _player;
    public GameObject _twinTransition;

    private Animator _TransitionAnimation;
   
    //public string twinName ="SegmentTransition";
    
    
    // Start is called before the first frame update
    void Start()
    {
        _TransitionAnimation = GameObject.FindGameObjectWithTag("Fade").GetComponent<Animator>();

        //transform.transform.GetChild(0).position = new Vector2(transform.transform.GetChild(0).position.x,);
        // _twinTransition = GameObject.Find(twinName);

    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _player = GameObject.FindGameObjectWithTag("Player");


            StartCoroutine(Animation());
           
            //if (_TransitionAnimation.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            //{  //If normalizedTime is 0 to 1 means animation is playing, if greater than 1 means finished
            //    Debug.Log("finished");
               

            //}
            //else
            //{
                
            //    Debug.Log("playing");
            //}
           
        }
    }
    IEnumerator Animation() 
    {
        _player.GetComponent<PlayerMovment>().StopMoving();
        _TransitionAnimation.Play("Fade_End");
        _player.transform.position = _twinTransition.transform.GetChild(0).position;
        yield return new WaitForSeconds(0.2f);
       
        _player.GetComponent<PlayerMovment>().StartMoving();
    }
}

