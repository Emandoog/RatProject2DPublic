using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public GameObject _LedgeCheck;
    public GameObject _GroundCheck;

    public SOEnemyStats _EnemyStats;
    public Transform _Target;
    private Vector2 target;

    public float health;

    public float movmentSpeed;
    public float jumpHeight;
    public bool canFly;
    public float aggroRange = 0f;
    public float playerDistance;
    public float nextWaypointDistance = 3f;
    public bool onGround;
    public bool grounded;

    public int currentWaypoint = 0;
    public bool reachedEndOfPath = false;

    public bool canMove;
    public float patrolRange = 6f;
    public bool chase = false;
    private LayerMask Ground;

    //private AIPath aIPath;
    private Transform _Player;
    private float maxHealth;
    private bool doOnce = true;
    private bool patrol;
    private Path path;
    private float jumpInPatrol;
    private float scale;
    Seeker _seeker;
    Rigidbody2D _RB2D;
    // Start is called before the first frame update
    void Start()
    {
        
        Ground = LayerMask.GetMask("Ground");
        _RB2D = GetComponent<Rigidbody2D>();
        _seeker = GetComponent<Seeker>();

        ReloadStats();
        health = maxHealth;
        jumpInPatrol = jumpHeight * 1.5f;

        if (!GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>())
        {
            Debug.Log("Could not find player ");
        }
        else
        {
            _Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }
        if (Vector2.Distance(_Player.position, _RB2D.position) <= aggroRange)
        {
           
            StartChase();
        }
        else
        {
            _Target = gameObject.transform;
            StartPatrol();

        }

        InvokeRepeating("FindPath", 2f, 0.2f);
        scale = transform.localScale.x;
    }

    // Update is called once per frame
    private void Update()
    {
        grounded = Physics2D.Raycast(_GroundCheck.transform.position, Vector2.down, 0.2f, Ground);
        onGround = Physics2D.Raycast(_LedgeCheck.transform.position, Vector2.down, 0.2f, Ground);
    }
    void FixedUpdate()
    {

        if (patrol)
        {
            
            if (reachedEndOfPath && canMove)
            {

                Debug.Log("reached end ");
                StartCoroutine(StoppedMoving());


            }
            if (!onGround && canMove && grounded && !reachedEndOfPath)
            {
                if (doOnce)
                {
                    Debug.Log("lol");
                    doOnce = false;
                    StartCoroutine(ChangeDirection2());
                    doOnce = true;
                }
                // StartCoroutine(StoppedMoving());

                  
                
            }
            if (playerDistance < aggroRange)
            {
                patrol = false;
                StartChase();

            }
        }
        if (chase)
        {

            if (reachedEndOfPath && canMove)
            {

                Debug.Log("reached end ");
               //StartCoroutine(StoppedMoving());
               //attack


            }

            if (playerDistance > aggroRange )
            {
                chase = false;
                StartPatrol();
            }
            
        }





        playerDistance = Vector2.Distance(_Player.position, _RB2D.position);

        if (path == null)
        {
            return;
        }
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - _RB2D.position).normalized;
        Vector2 Force = direction * movmentSpeed * Time.deltaTime;

        if (canMove)
        {
            _RB2D.AddForce(Force);
            if (direction.y > 0.6f && grounded)
            {
                 Debug.Log("Jump");
                if (patrol)
                {
                    _RB2D.AddForce((Vector2.up * jumpInPatrol),ForceMode2D.Impulse);
                }
                else 
                { 
                  _RB2D.AddForce((Vector2.up * jumpHeight),ForceMode2D.Impulse);
                }
            }
        }

        float distance = Vector2.Distance(_RB2D.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        if (_RB2D.velocity.x >= 0.1 && canMove)
        {
            transform.localScale = new Vector3(scale, scale, 1f);
        }
        else if (_RB2D.velocity.x <= -0.1f && canMove)
        {
            transform.localScale = new Vector3(-scale, scale, 1f);
        }

    }
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    public void ReloadStats()
    {
        maxHealth = _EnemyStats.maxhealth;

        movmentSpeed = _EnemyStats.movmentSpeed;
        jumpHeight = _EnemyStats.jumpHeight;

    }
    public void FindPath()
    {

        if (_seeker.IsDone())
        {
            if (!chase)
            {
                _seeker.StartPath(_RB2D.position, target, OnPathComplete);
            }
            else 
            {
                _seeker.StartPath(_RB2D.position, _Target.position, OnPathComplete);
            }
        }
    }
    public void StartChase()
    {
        _Target = _Player;
        Debug.Log("Chase Started");
        chase = true;
        canMove = true;


    }
    public void StartPatrol()
    {
        Debug.Log("Patrol Started");
        patrol = true;

        StartCoroutine(Firstpoint());


        //Go either left or right 
        // stop when there is no more ground 
        // stop when destination is reached


    }
    IEnumerator StoppedMoving()
    {
        canMove = false;
        _RB2D.velocity = Vector2.zero;
        FindRandomPatrolPoint(patrolRange);
        yield return new WaitForSeconds(2f);

        canMove = true;


    }
    IEnumerator Firstpoint()
    {

        canMove = false;
        yield return new WaitForSeconds(1f);
        if (onGround)
        {
            FindRandomPatrolPoint(patrolRange);
            canMove = true;
        }
        else 
        {
            StartCoroutine(Firstpoint());
        }

        


    }

    void FindRandomPatrolPoint(float range)
    {

        //_Target.position = new Vector2(Random.Range(-range, range) + gameObject.transform.position.x, gameObject.transform.position.y);
        target = new Vector2(Random.Range(-range, range) + gameObject.transform.position.x, gameObject.transform.position.y);
        //Debug.Log("FindRandomPoint" + target);
    }
    private void ChangeDirection()
    {
        canMove = false;
        Debug.Log("ChangeDirection" + " reached Ledge");
        _RB2D.velocity = new Vector2(0f, 0f);

        float distanceLeft = Vector2.Distance(gameObject.transform.position, target);
        distanceLeft = distanceLeft * scale * -1f;
        Debug.Log("ChangeDirection" + target);
        Debug.Log("ChangeDirection" + distanceLeft);
        target = new Vector2((transform.position.x + distanceLeft), target.y);
        _seeker.StartPath(_RB2D.position, target, OnPathComplete);
        Debug.Log("ChangeDirection" + target);
        if (!onGround)
        {
            transform.localScale = new Vector3(scale * -1f, scale, 1f);
        }

        canMove = true;
    }
    IEnumerator ChangeDirection2()
    {

        canMove = false;
       // Debug.Log("ChangeDirection" + " reached Ledge");
        _RB2D.velocity = new Vector2(-0.5f * _RB2D.velocity.x, _RB2D.velocity.y);
        
        float distanceLeft = Vector2.Distance(gameObject.transform.position, target);
        distanceLeft = distanceLeft * scale * -1f;
       // Debug.Log("ChangeDirection" + target);
        //Debug.Log("ChangeDirection" + distanceLeft);
        target = new Vector2((transform.position.x + distanceLeft), target.y);
        _seeker.StartPath(_RB2D.position, target, OnPathComplete);
       // Debug.Log("ChangeDirection" + target);
        if (!onGround)
        {
            transform.localScale = new Vector3(scale * -1f, scale, 1f);
        }
        canMove = true;
        yield return new WaitForSeconds(0.1f);
    }
    void OnDrawGizmosSelected()
    {
        // Draws a 5 unit long red line in front of the object
        Gizmos.color = Color.red;
       // Vector3 direction = _LedgeCheck.transform.TransformDirection(Vector3.down) *5f;
        Gizmos.DrawRay(_LedgeCheck.transform.position, Vector2.down * 1f);
    }
    IEnumerator FindPlayer() 
    {
        yield return new WaitForSeconds(0f);
    
    
    }
}
    
