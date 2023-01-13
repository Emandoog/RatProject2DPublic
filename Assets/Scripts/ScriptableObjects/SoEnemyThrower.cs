using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EnemyThrower", menuName = "Enemy/EnemyThrower", order = 1)]
public class SOEnemyThrower : ScriptableObject
{
    public float Life;
    public float damage;
    public float flaskThrowHeight;
    public float flaskThrowPower;
}

