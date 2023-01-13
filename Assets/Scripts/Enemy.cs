using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float life;
    private Rigidbody2D _RB2D;
    private GameObject _Player;
    public bool canBeknockBack = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _RB2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void TakeDamage(float damage) 
    {
        life -= damage;
         if(life < 0f)
        {
            Die();

        }
    }
    public void Die()
    {
        //play die  animation
        //death state
        _RB2D.constraints = RigidbodyConstraints2D.FreezeAll;
        gameObject.layer = 9;
        Destroy(gameObject, 1f);
    }

    private void OnDisable()
    {
        Destroy(gameObject);
    }
    public void KnockBack()
    {
        if (canBeknockBack)
        { 
        _Player = GameObject.FindGameObjectWithTag("Player");
        
        
          
            if (transform.position.x > _Player.transform.position.x)
            {

                _RB2D.AddForce(new Vector2(0, 2f), ForceMode2D.Impulse);
                _RB2D.AddForce(new Vector2(200f, 0), ForceMode2D.Force);

            }
            if (transform.position.x < _Player.transform.position.x)
            {
                
                _RB2D.AddForce(new Vector2(0f, 2f), ForceMode2D.Impulse);
                _RB2D.AddForce(new Vector2(-200f, 0), ForceMode2D.Force);

            }
        }



    }
    public void SetMaxLife(float MaxLife) 
    {
        life = MaxLife;
    
    }
}
