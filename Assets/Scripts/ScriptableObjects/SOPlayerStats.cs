using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Player/PlayerStats", order = 1)]
public class SOPlayerStats : ScriptableObject
{
    //public string prefabName;
    public float health;
    public float maxHealth;
    public float movmentSpeed;
    public float score;
    public float jumpHeight;
    public float jumpsInAir;
    public float airControll;
    public float jumpMulti;
    public float fallMulti;
    public float dashSpeed;
    public bool canMove;

    
}