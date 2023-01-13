using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public ParticleSystem _Poison;
    public bool poisoned = false;
    [Header("Variables")]
    public SOPlayerStats _PlayerStats;
    public GameObject LifeBar;
    // public PlayerMovment _Playermovment;
    public GameObject _Warrning;

    private float movmentDirection;
    private Rigidbody2D _RB2D;
    private bool inHazard = false;
    //private float jump;
    // Start is called before the first frame update
    void Start()
    {
       // _Poison.Stop();
        _PlayerStats.canMove = true;
        ReloadStats();
        _PlayerStats.health = _PlayerStats.maxHealth;
        _RB2D = GetComponent<Rigidbody2D>();
        if (LifeBar != null)
        {
            LifeBar.GetComponent<LifeBar>().UpdateLifeBar();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
        //if (Input.GetKeyDown("f"))
        //{
        //    PlayerTakeDamage(50f);
        //    ReloadStats();
        //    //_Poison.Play();
        //}
        if (poisoned)
        {
           LifeBar.GetComponent<LifeBar>().StartPoison();
            _Poison.Play();

        } else if(!poisoned)
        {
            LifeBar.GetComponent<LifeBar>().StopPoison();
            _Poison.Stop();

            //_Poison.Clear();
        }

        

    }
   




    public void ReloadStats()
    {


    }
    public void PlayerTakeDamage(float DamageTaken)
    {
        _PlayerStats.health -= DamageTaken;
        if (LifeBar != null)
        {
            LifeBar.GetComponent<LifeBar>().UpdateLifeBar();
        }
        if (_PlayerStats.health <= 0)
        {
            SceneManager.LoadScene(0);
            Debug.Log("You died.");

        }
      _PlayerStats.health =  Mathf.Clamp(_PlayerStats.health, 0, _PlayerStats.maxHealth);
    }
    public void PlayerTakeDotDamage(float damage=1,float duration = 1f)
    {
       
        StartCoroutine(DotDamage(damage,duration));
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(Protected(0.25f, 1f));
            if (transform.position.x > collision.transform.position.x)
            {

                // _rb2d.velocity = new Vector2(0f, 0f);
                _RB2D.AddForce(new Vector2(0, 13f), ForceMode2D.Impulse);
                _RB2D.AddForce(new Vector2(700f, 0), ForceMode2D.Force);
                //= new Vector2(40f, 10f);

            }
            if (transform.position.x < collision.transform.position.x)
            {
                //StartCoroutine(Protected(0.25f,0.25f));
                // _rb2d.velocity = new Vector2(0f, 0f);
                _RB2D.AddForce(new Vector2(0f, 13f), ForceMode2D.Impulse);
                _RB2D.AddForce(new Vector2(-700f, 0), ForceMode2D.Force);
                //= new Vector2(40f, 10f);

            }

            PlayerTakeDamage(5f);
        }
    }
    //private void OnCollisionEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Enemy"))
    //    {
    //        StartCoroutine(Protected(0.25f, 1f));
    //        if (transform.position.x > collision.transform.position.x)
    //        {

    //            // _rb2d.velocity = new Vector2(0f, 0f);
    //            _RB2D.AddForce(new Vector2(0, 15f), ForceMode2D.Impulse);
    //            _RB2D.AddForce(new Vector2(1000f, 0), ForceMode2D.Force);
    //            //= new Vector2(40f, 10f);

    //        }
    //        if (transform.position.x < collision.transform.position.x)
    //        {
    //            //StartCoroutine(Protected(0.25f,0.25f));
    //            // _rb2d.velocity = new Vector2(0f, 0f);
    //            _RB2D.AddForce(new Vector2(0f, 15f), ForceMode2D.Impulse);
    //            _RB2D.AddForce(new Vector2(-1000f, 0), ForceMode2D.Force);
    //            //= new Vector2(40f, 10f);

    //        }

    //        PlayerTakeDamage(5f);
    //    }

    //}
    IEnumerator Protected(float stagger, float addtionalprotection = 0)

    {

        gameObject.layer = 9;
        GetComponent<PlayerMovment>().StopMoving();
        GetComponent<PlayerAttack>().ForbidAttack();
        yield return new WaitForSeconds(stagger);
        GetComponent<PlayerMovment>().StartMoving();
        GetComponent<PlayerAttack>().AllowAttack();
        yield return new WaitForSeconds(addtionalprotection);
        gameObject.layer = 7;

    }
    IEnumerator DotDamage(float DotDamage, float DotTime)
    {
        
        //_Poison.Play();
        
        float damagePerTick = DotDamage / DotTime;
        float damageDealt = 0;
        while (damageDealt < DotDamage) 
        {
            poisoned = true;
            yield return new WaitForSeconds(1f);
            PlayerTakeDamage(damagePerTick);
            damageDealt += damagePerTick;
           
        }
        yield return new WaitForSeconds(0.5f);
        poisoned = false;
    

    }
    IEnumerator Hazard(float HazardDamage)
    {

        yield return new WaitForSeconds(1f);
       
        if (inHazard)
        {
            PlayerTakeDamage(HazardDamage);
            StartCoroutine(Hazard(HazardDamage));
        }

    }
    public void EnterHazard(float HazardDamage) 
    {
       

        if (!inHazard) 
        {
            _Warrning.SetActive(true);
            inHazard = true;
            StartCoroutine(Hazard(HazardDamage));
        }

    }
    public void LeaveHazard()
    {
        _Warrning.SetActive(false);
        Debug.Log("LeaveHAzard");
        inHazard = false;
    }

}
        //IEnumerator FallFromPLatform(float stagger)

//{
//    gameObject.layer = 10;
//    Hitted = true;
//    yield return new WaitForSeconds(stagger);
//    Hitted = false;
//    gameObject.layer = 7;
//}

