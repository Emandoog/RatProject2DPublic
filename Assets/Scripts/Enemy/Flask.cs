using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flask : MonoBehaviour
{
    private Rigidbody2D _RB2D;
    private GameObject _Player;
    public GameObject _FlaskEffect;
    
    private int direction = 1;
    public SOEnemyThrower _UnitStats;
    void Start()
    {
        _Player = GameObject.FindGameObjectWithTag("Player");
        _RB2D = GetComponent<Rigidbody2D>();
        if (transform.position.x > _Player.transform.position.x) //left
        {

            direction = -1;

        }
        if (transform.position.x < _Player.transform.position.x) //right
        {
            direction = 1;

        }
        FlaskVeloctiy();
       

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //dissapear
        //make some nice effects
        // deal damage
        Instantiate(_FlaskEffect,transform.position,transform.rotation);
        Debug.Log("Hitground");
        Destroy(gameObject);
      
    }
    
    private void FlaskVeloctiy() 
    {
       if (GetComponentInParent<RatThrowerAI>() != null)
        { 
            
            _RB2D.velocity = new Vector2(direction * GetComponentInParent<RatThrowerAI>().flaskThrowPower, GetComponentInParent<RatThrowerAI>().flaskThrowHeight);
         }
    
    }

    

}
