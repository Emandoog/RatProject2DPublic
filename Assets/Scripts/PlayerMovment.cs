using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovment : MonoBehaviour
{
    public bool grounded;

    private bool groundCheck1;
    private bool groundCheck2;
    private float airControll;
    private float gravity;
    private float jumpCounter = 1f ;
    public float CheckRadius = 1.2f;
    private float movmentSpeed;
    private float jumpHeight;
    private float jumpsInAir;
    private float jumpMulti;
    private float fallMulti;
    private float scale;
    private float dashSpeed;
    private bool dashing = false;
    private bool canMove = true;
    private bool dashOnCD = false;
    [SerializeField] private GameObject _Timer;
    [SerializeField] private GameObject GroundCheck;
    [SerializeField] private GameObject GroundCheck2;
    [SerializeField] private LayerMask Ground;
    public Animator _PlayerAnimator;
    [Header("Variables")]
    public SOPlayerStats _PlayerStats;

    

    private float movmentDirection = 1;
    private Rigidbody2D _rb2d;
    //private float jump;
    // Start is called before the first frame update
    void Start()
    {
       
        //_PlayerAnimator = GetComponent<Animator>();
        Vector3 direction = transform.localScale;
        scale = direction.x;

        StartCoroutine(ReloadStatsTimer()); //Load stats from scriptable object once every second.
                                            // This propably needs to be changed later.
        jumpCounter = jumpsInAir;
        _rb2d = GetComponent<Rigidbody2D>();
        gravity = _rb2d.gravityScale;
    }

   
    void Update()
    {
        
        //canMove = _PlayerStats.canMove;

        //scale = direction.x;
        

        #region Movment
        if (canMove == true)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) )
            {
                Dash();


            }
            Vector3 direction = transform.localScale;
            if (Time.timeScale == 1f) 
            {
                if (Input.GetKey("a") && !Input.GetKey("d"))
                {
                    direction.x = scale * -1f;


                }
                if (Input.GetKey("d") && !Input.GetKey("a"))
                {
                    direction.x = scale;
                }
            }
            transform.localScale = direction;
            //check if the player is on the ground or not 



            //movmentDirection = Input.GetAxis("Horizontal");


            //Make the player jump
            if (Input.GetKeyDown("space") && grounded)
            {
                //Jump();
                //_rb2d.AddForce(new Vector2(_rb2d.velocity.x, jumpHeight ),ForceMode2D.Force);
                _rb2d.velocity = new Vector2(_rb2d.velocity.x, jumpHeight);

            }
            else if (Input.GetKeyDown("space") && jumpCounter > 0f && grounded == false)
            {

                //Decrase jump counter
                jumpCounter -= 1f;
                //This makes second jump more responsive
                _rb2d.velocity = new Vector2(_rb2d.velocity.x, jumpHeight * 1f);
                //_rb2d.AddForce(new Vector2(_rb2d.velocity.x + jumpHeight * movmentDirection, jumpHeight * 200f), ForceMode2D.Force);
            }

            //Make the jump feel better
            if (_rb2d.velocity.y < 0)
            {

                _rb2d.velocity += Vector2.up * Physics.gravity * fallMulti * Time.deltaTime;


            }
            else if (_rb2d.velocity.y > 0 && !Input.GetKeyDown("space"))
            {
                _rb2d.velocity += Vector2.up * Physics.gravity * jumpMulti * Time.deltaTime;
            }
        }
        #endregion

        _PlayerAnimator.SetFloat("Speed", Mathf.Abs(_rb2d.velocity.x));
        _PlayerAnimator.SetFloat("VelocityY", _rb2d.velocity.y);
        if (grounded != true)
        {
            _PlayerAnimator.SetBool("Landed", false);
            _PlayerAnimator.SetBool("IsInAir", true);


        }
        else
        {
            _PlayerAnimator.SetBool("IsInAir", false);


        }
    }
    void FixedUpdate()
    {
        if (dashing)
        {


            //_rb2d.AddForce(new Vector2(movmentDirection * dashSpeed, 0));
            _rb2d.velocity = new Vector2(movmentDirection * dashSpeed, 0);

        }

        groundCheck1 = Physics2D.Raycast(GroundCheck.transform.position, transform.TransformDirection(Vector2.down), CheckRadius, Ground);
        groundCheck2 = Physics2D.Raycast(GroundCheck2.transform.position, transform.TransformDirection(Vector2.down), CheckRadius, Ground);
        if (groundCheck1 || groundCheck2)
        {
            grounded = true;
        }
        else
        {
            grounded = false;        
        }
        //grounded = Physics2D.OverlapCircle(GroundCheck.transform.position, CheckRadius, Ground);

        #region Movment
        if (Input.GetKey("a") && !Input.GetKey("d") && canMove == true)
        {

            movmentDirection = -1;
            _rb2d.AddForce(new Vector2(movmentDirection * movmentSpeed, _rb2d.velocity.y));
           //_rb2d.velocity =(new Vector2(movmentDirection * movmentSpeed, _rb2d.velocity.y));

        }
        if (Input.GetKey("d") && !Input.GetKey("a") && canMove == true)
        {

            movmentDirection = 1;
            _rb2d.AddForce(new Vector2(movmentDirection * movmentSpeed, _rb2d.velocity.y));
           // _rb2d.velocity = (new Vector2(movmentDirection * movmentSpeed, _rb2d.velocity.y));
        }

        //Friction needs to be set to 0, otherwise player will stick to walls if the key is pressed


        //Limit the velocity 
        if (_rb2d.velocity.x > 7f && !dashing)
        {
            _rb2d.velocity = new Vector2(7f, _rb2d.velocity.y);
        }

        if (_rb2d.velocity.x < -7f && !dashing)
        {
            _rb2d.velocity = new Vector2(-7f, _rb2d.velocity.y);
        }
        if (canMove == false)
        {




        }
        #endregion
       

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Ground") )
        {
           // grounded = true;
           
            jumpCounter = jumpsInAir;
            
        }
    }
  
    private void OnCollisionExit2D(Collision2D collision)
    {
       
        if (collision.gameObject.CompareTag("Ground")  )
        {
           // grounded = false;
            jumpCounter = jumpsInAir;

        }
    }
    public void Jump()
    {
        _rb2d.velocity = new Vector2(_rb2d.velocity.x, jumpHeight);
    }

    public void ReloadStats()
    {


        movmentSpeed = _PlayerStats.movmentSpeed;
        jumpHeight = _PlayerStats.jumpHeight;
        jumpsInAir = _PlayerStats.jumpsInAir;
        fallMulti = _PlayerStats.fallMulti;
        jumpMulti = _PlayerStats.jumpMulti;
        airControll = _PlayerStats.airControll;
        dashSpeed = _PlayerStats.dashSpeed;

    }

    public void StopMoving(bool freeze = false) 
    {
        canMove = false;
        if (freeze)
        {
            _rb2d.velocity = new Vector2(0, 0);
        }
    }
    public void FreezeX()
    {
        
            _rb2d.velocity = new Vector2(0, 0);
        
    }
    public void StartMoving()
    {
        canMove = true;
    }
    

    IEnumerator DashCD(float dashTimer = 1f)
    {
        dashOnCD = true;
       
        yield return new WaitForSeconds(dashTimer);
        _Timer.SetActive(false);
        dashOnCD = false;


    }
    public void Dash()
    {
       

        if (canMove == true && dashOnCD != true)
        {
            
            _rb2d.velocity = new Vector2(0, 0); // stop the player after the dash ends 
            dashOnCD = true;
            canMove = false;
            dashing = true;
            gameObject.layer = 9;
            _rb2d.gravityScale = 0;
            _PlayerAnimator.SetBool("Dashing", true);
            _Timer.SetActive(true);





        }


    }

    IEnumerator ReloadStatsTimer(float timer = 1f)
    {
        ReloadStats();
        yield return new WaitForSeconds(timer);
        StartCoroutine(ReloadStatsTimer());
    }


    public void StartDash()
    { 
    
    
    
    }
    public void StopDash()
    {
        dashing = false;
        _PlayerAnimator.SetBool("Dashing", false);
       
        canMove = true;
        gameObject.layer = 7;
        _rb2d.velocity = new Vector2(0, 0); // stop the player after the dash ends 
        _rb2d.gravityScale = gravity;
        StartCoroutine(DashCD());

    }
    public void Landed()
    {

        _PlayerAnimator.SetBool("Landed", true);

    }
    private void OnDrawGizmos()
    {
        
        Gizmos.color = Color.red;
        
        Gizmos.DrawRay(GroundCheck.transform.position, Vector2.down * CheckRadius);
        Gizmos.DrawRay(GroundCheck2.transform.position, Vector2.down * CheckRadius);
    }

    //IEnumerator FallFromPLatform(float stagger)

    //{
    //    gameObject.layer = 10;
    //    Hitted = true;
    //    yield return new WaitForSeconds(stagger);
    //    Hitted = false;
    //    gameObject.layer = 7;
    //}
}

