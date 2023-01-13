using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : MonoBehaviour
{
    private Rigidbody2D _RB2D;
    private GameObject _Player;
    private float scale;

    private int direction = 1;
    //public SOEnemyThrower _UnitStats;
    void Start()
    {
        _Player = GameObject.FindGameObjectWithTag("Player");
        _RB2D = GetComponent<Rigidbody2D>();
        scale = transform.localScale.x;
        if (transform.position.x > _Player.transform.position.x) //left
        {

            direction = -1;
            transform.localScale = new Vector3(scale, scale, 1f);

        }
        if (transform.position.x < _Player.transform.position.x) //right
        {
            direction = 1;
            transform.localScale = new Vector3(-scale, scale, 1f);
        }
        DaggerVelocity();


    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().PlayerTakeDamage(GetComponentInParent<RatThrowerAI>().attakDamage);
            Destroy(gameObject);


        }
        if (collision.gameObject.CompareTag("Ground"))
        {

            //GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().PlayerTakeDamage(GetComponentInParent<RatThrowerAI>().attakDamage);
            //Destroy(gameObject);
            _RB2D.velocity = Vector2.zero;
            gameObject.layer = 9;
            Destroy(gameObject,3f);

        }
    }
    private void DaggerVelocity()
    {
        if (GetComponentInParent<RatThrowerAI>() != null)
        {

            _RB2D.velocity = new Vector2(direction * GetComponentInParent<RatThrowerAI>().flaskThrowPower, 0f);

        }
    }
}

