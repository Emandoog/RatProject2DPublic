using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventController : MonoBehaviour
{




    public void LandCommand()
    {
        GetComponentInParent<PlayerMovment>().Landed();

    }
    public void StopMovmentCommand()
    {

        GetComponentInParent<PlayerMovment>().StopMoving();
    }
    public void StartMovmentCommand()
    {

        GetComponentInParent<PlayerMovment>().StartMoving();
    }
    public void StartDashCommand()
    {

        GetComponentInParent<PlayerMovment>().StartDash();
    }
    public void StopDashCommand()
    {

        GetComponentInParent<PlayerMovment>().StopDash();
    }
    public void FreezeXCommand()
    {

        GetComponentInParent<PlayerMovment>().FreezeX();
    }









    public void StartAttackingCommand()
    {
        GetComponentInParent<PlayerAttack>().StartAttacking();
    }
    public void StopAttackingCommand()
    {
        GetComponentInParent<PlayerAttack>().StopAttacking();
    }
    public void DealDamageCommand()
    {
        GetComponentInParent<PlayerAttack>().DealDamage();

    }
    public void AllowAttackCommand()
    {
        GetComponentInParent<PlayerAttack>().AllowAttack();

    }
    public void ForbidAttackCommand()
    {
        GetComponentInParent<PlayerAttack>().ForbidAttack();

    }

}
