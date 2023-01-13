using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAnimationEvents : MonoBehaviour
{
    public void JumpCommand() 
    {

        GetComponentInParent<SlimeAI>().Jump(); 
    
    }
    public void StartMovingCommand()
    {

        GetComponentInParent<SlimeAI>().StartMoving();

    }
    public void StopMovingCommand()
    {

        GetComponentInParent<SlimeAI>().StopMoving();

    }
    public void StopVelocityCommand()
    {

        GetComponentInParent<SlimeAI>().StopVelocity();

    }
    public void MoveBackCommand()
    {

        GetComponentInParent<SlimeAI>().MoveBack();

    }
}
