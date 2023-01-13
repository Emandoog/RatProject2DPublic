using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BatAi : MonoBehaviour
{
    private Seeker _Seeker;
    private Rigidbody2D _RB2D;
    public Transform target;
    private GameObject _Player;
    public float movmentSpeed = 200f;
    public float nextWaypointDistance = 3f;
    public bool canMove = true;
    private Path path;
    private  int currentWaypoint = 0;
    private bool reachedEndOfPath = false;

    public float aggroRange = 10f;
    // Start is called before the first frame update
    void Start()
    {
       
        _RB2D = GetComponent<Rigidbody2D>();
        _Seeker = GetComponent<Seeker>();
        _Player = GameObject.FindGameObjectWithTag("Player");
        _Seeker.StartPath(_RB2D.position, _Player.transform.position, OnPathComplete);

        InvokeRepeating("FindPath", 2f, 0.2f);

    }

    
    void FixedUpdate()
    {
        if (Vector2.Distance(_Player.transform.position, _RB2D.position) <= aggroRange)
        {

            
        }

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

        float distance = Vector2.Distance(_RB2D.position, path.vectorPath[currentWaypoint]);
      
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        if (canMove)
        {
            _RB2D.AddForce(Force);
   
        }

        if (_RB2D.velocity.x >= 0.1 && canMove)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (_RB2D.velocity.x <= -0.1f && canMove)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }





    public void OnPathComplete(Path p)
    {
        if (!p.error)
        {

            path = p;
            currentWaypoint = 0;
        }
    
    }
    public void FindPath()
    {

        if (_Seeker.IsDone())
        {
            _Seeker.StartPath(_RB2D.position, _Player.transform.position, OnPathComplete);
        }
    }
}
