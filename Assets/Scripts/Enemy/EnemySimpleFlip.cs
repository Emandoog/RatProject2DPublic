using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySimpleFlip : MonoBehaviour
{
    private GameObject _Player;
    private float scale;
    // Start is called before the first frame update
    void Start()
    {
        _Player = GameObject.FindGameObjectWithTag("Player");
        scale = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
       


        if (transform.position.x > _Player.transform.position.x) //left
        {

            transform.localScale = new Vector3(-scale, scale, 1f);

        }
        if (transform.position.x < _Player.transform.position.x) //right
        {
            transform.localScale = new Vector3(scale, scale, 1f);

        }
    }
}
