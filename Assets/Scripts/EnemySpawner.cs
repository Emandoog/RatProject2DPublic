using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject _enemyToSpawn;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("EnemySpawned");
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SpawnEnemy()

    {
        var enemy = Instantiate(_enemyToSpawn, transform.position, transform.rotation);
        enemy.transform.parent = transform;
        Debug.Log("EnemySpawned");

    }
    private void OnEnable()
    {
        SpawnEnemy();
    }

}
