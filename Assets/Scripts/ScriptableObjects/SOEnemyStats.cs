using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Enemy/EnemyStats", order = 1)]

public class SOEnemyStats : ScriptableObject
{
   public float movmentSpeed;
   public float attackDamage;
   public float maxhealth;
   public float jumpHeight;
   public bool flying;
}
