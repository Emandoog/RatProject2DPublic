using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySlime", menuName = "Enemy/EnemySlime", order = 1)]
public class SOEnemySlime : ScriptableObject
{
    public float Life;
    public float damage;
    public float jumpHeightPower;
    public float jumpPower;
}


