using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionPickUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().PlayerTakeDamage(-24f);
            Destroy(gameObject);

        }
    }
    //private void OnTriggerEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {

    //        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().PlayerTakeDamage(-24f);
    //        Destroy(gameObject);

    //    }
    //}

}
