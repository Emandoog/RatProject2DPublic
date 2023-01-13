using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonDamage : MonoBehaviour
{
    private bool poisoned = false;
    public bool timed = true;
    public float duration = 6f;
    public bool isHazard = false;
    public float dotDamage = 5f;
    public float dotTime = 2f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            if (isHazard != true)
            {
                if (!poisoned)
                {
                    poisoned = true;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().PlayerTakeDotDamage(dotDamage, dotTime);

                }
            }
            else 
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().EnterHazard(dotDamage);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (isHazard)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().LeaveHazard();
            }
        }   
    }
    private void Start()
    {
        if (timed)
        { 
            Destroy(gameObject,duration);
        }
    }
}
