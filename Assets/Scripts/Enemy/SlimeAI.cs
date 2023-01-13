using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAI : MonoBehaviour
{
    public SOEnemySlime _UnitStats;
    public float jumpPower;
    public float jumpHeightPower;
    public Animator _EnemyAnimator;
    public float attakDamage;
    public float aggroRange = 7f;
    public LayerMask Ground;
    public GameObject _GroundCheck1;
    public GameObject _GroundCheck2;
    public bool poisoned = false;
    public ParticleSystem _Poison;
    public SpriteRenderer _Sprite;

    private int direction;
    private bool groundCheck1;
    private bool groundCheck2;
    private float life;
    private bool idle;
    private bool moving;
    private Rigidbody2D _RB2D;
    private GameObject _Player;
    private float distance;
    private float scale;
    // Start is called before the first frame update
    void Start()
    {
        _Sprite.color = new Color(49, 86, 23);
        _RB2D = GetComponent<Rigidbody2D>();
        life = _UnitStats.Life;
        _Player = GameObject.FindGameObjectWithTag("Player");
       
        GetComponentInParent<Enemy>().SetMaxLife(life);
        //InvokeRepeating("ReloadStats", 0f, 1f);
        _Poison.Stop();
        scale = transform.localScale.x;
    }
    private void Update()
    {

        if (poisoned)
        {
            
            _Poison.Play();
        }
        else
        {
          
            _Poison.Stop();

        }        
        distance = Vector2.Distance(_Player.transform.position, gameObject.transform.position);
        if (distance > aggroRange)
        {

            _EnemyAnimator.SetBool("Idle", true);

        }
        else
        {

            _EnemyAnimator.SetBool("Idle", false);
        }
        

        _EnemyAnimator.SetFloat("Velocity", (_RB2D.velocity.y));

        if (moving == true)
        {
            
            if (transform.position.x > _Player.transform.position.x) //left
            {
                
                direction = -1;
                transform.localScale = new Vector3(-scale, scale, 1f);

            }
            if (transform.position.x < _Player.transform.position.x) //right
            {
                direction = 1;
                transform.localScale = new Vector3(scale, scale, 1f);

            }
        }
    }
    private void FixedUpdate()
    {
        groundCheck1 = Physics2D.Raycast(_GroundCheck1.transform.position, transform.TransformDirection(Vector2.down), 0.2f, Ground);
        groundCheck2 = Physics2D.Raycast(_GroundCheck2.transform.position, transform.TransformDirection(Vector2.down), 0.2f, Ground);
        if (groundCheck1 || groundCheck2)
        {
            _EnemyAnimator.SetBool("OnGround", true);
        }
        else
        {
            _EnemyAnimator.SetBool("OnGround", false);
        }
    }


    //public void ReloadStats()
    //{
    //    jumpPower = _UnitStats.jumpPower;
    //    jumpHeightPower = _UnitStats.jumpHeightPower;
    //    attakDamage = _UnitStats.damage;

    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Poison"))
        {
            Poisoned();

        }
        if (poisoned)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                _Player.GetComponent<PlayerManager>().PlayerTakeDotDamage(10f, 4f);

            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (poisoned) 
        { 
            if (collision.gameObject.CompareTag("Player"))
            {
                _Player.GetComponent<PlayerManager>().PlayerTakeDotDamage(10f,4f);

            }
        }
    }
    public void Jump() 
    {
        float Height = _Player.transform.position.y - gameObject.transform.position.y;
        
        if (Height>0.5f)
        {
            _RB2D.velocity = new Vector2(direction * _UnitStats.jumpPower * 0.7f, _UnitStats.jumpHeightPower * 1.3f);

        }
        else 
        {
            _RB2D.velocity = new Vector2(direction * _UnitStats.jumpPower, _UnitStats.jumpHeightPower);


        }

    }
    public void StartMoving()
    {
        moving = true;
    
    
    }
    public void StopMoving()
    {
        moving = false;


    }
    public void StopVelocity() 
    {
        _RB2D.velocity = Vector2.zero;
    
    }
    public void MoveBack()
    {

        _RB2D.AddForce(new Vector2(-direction*80f,0));


    }
    public void StartAttacking()
    {
        _EnemyAnimator.SetBool("Attacking", true);


    }
    public void StopAttacking()
    {
        _EnemyAnimator.SetBool("Attacking", false);


    }
    public void Poisoned()
    {
        
        
        poisoned = true;
    
    
    }
    private void OnDrawGizmos()
    {

        Gizmos.color = Color.red;

        Gizmos.DrawRay(_GroundCheck1.transform.position, Vector2.down * 0.2f);
        Gizmos.DrawRay(_GroundCheck2.transform.position, Vector2.down * 0.2f);
    }
}
