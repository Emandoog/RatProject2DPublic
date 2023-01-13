using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public SOPlayerSkills _PlayerSkills;
    public Transform _attackPosition;
    public Transform _attackPosition2;
    public BoxCollider2D _AttackZone;
    
    public LayerMask enemyToHit;
    public ArrayList enemiesHit;
    public ContactFilter2D objectsToHit;

    private bool attacking = false;
    private float attackDamage;
    private float attackrange;
    private bool canAttack = true;
    private Animator _PlayerAnimator;
    // private Collider2D EnemiesHit;
    // Start is called before the first frame update
    void Start()
    {
        _PlayerAnimator = GetComponentInChildren<Animator>();
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !attacking && canAttack )
        {
            Attack();


        }
       
    }

   
   
    //IEnumerator Attack()
    //{
    //    //GetComponent<PlayerMovment>().StopMoving(true);
    //    _PlayerAnimator.SetBool("Attacking", true);

    //   // attacking = true;
    //    Collider2D[] EnemiesHit = Physics2D.OverlapCircleAll(_attackPosition.position, _PlayerSkills.attackRange, enemyToHit);
    //    Debug.Log("Attack!");

    //    foreach (Collider2D Enemy in EnemiesHit)
    //    {
    //        Enemy.GetComponent<Enemy>().TakeDamage(_PlayerSkills.attackDamage);
    //        Debug.Log("Hit" + Enemy.name);
    //    }
    //    yield return new WaitForSeconds(0.2f);// this needs to be the same lenght of the attack animation 
    //   // attacking = false;

    //    //GetComponent<PlayerMovment>().StartMoving();
    //    _PlayerAnimator.SetBool("Attacking", false);
    //}
    public void Attack()
    {

        _PlayerAnimator.SetBool("Attacking", true);

    }

    public void DealDamage()
    {

        //Collider2D[] EnemiesHit = Physics2D.OverlapCircleAll(_attackPosition.position, _PlayerSkills.attackRange, enemyToHit);
        Collider2D[] EnemiesHit = Physics2D.OverlapAreaAll(_attackPosition.position,_attackPosition2.position, enemyToHit);
        Debug.Log("Attack!");

        foreach (Collider2D Enemy in EnemiesHit)
        {
            Enemy.GetComponent<Enemy>().TakeDamage(_PlayerSkills.attackDamage);
            Enemy.GetComponent<Enemy>().KnockBack();
            Debug.Log("Hit" + Enemy.name);
        }

    }


    public void AllowAttack()
    {

        canAttack = true;
    
    }
    public void ForbidAttack()
    {

        canAttack = false;

    }
    public void StartAttacking()
    {
        attacking = true;



    }
    public void StopAttacking()
    {
        _PlayerAnimator.SetBool("Attacking", false);
        attacking = false;
    }
}
