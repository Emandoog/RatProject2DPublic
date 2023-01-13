using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatThrowerAI : MonoBehaviour
{

    public SOEnemyThrower _UnitStats; 
    public GameObject _Flask;
    private GameObject _Player;
    private float life;
    public float flaskThrowHeight = 2f;
    public float flaskThrowPower = 4f;
    public float attackTimerr = 2f;
    public float attakDamage;
    // Start is called before the first frame update
    void Start()
    {
        life = _UnitStats.Life; 
        _Player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(ThrowFlask(attackTimerr));
        GetComponentInParent<Enemy>().SetMaxLife(life);
        InvokeRepeating("ReloadStats", 0f, 1f);
    }

   
   

    IEnumerator ThrowFlask( float attackTimer = 1f)
    {

        yield return new WaitForSeconds(attackTimer);
        var clone1 = Instantiate(_Flask,transform);
        clone1.transform.parent = transform;
        Debug.Log("throw");
        StartCoroutine(ThrowFlask(attackTimerr));
       
    }
    public void ReloadStats()
    {
        flaskThrowHeight = _UnitStats.flaskThrowHeight;
        flaskThrowPower = _UnitStats.flaskThrowPower;
        attakDamage = _UnitStats.damage;

    }
}
