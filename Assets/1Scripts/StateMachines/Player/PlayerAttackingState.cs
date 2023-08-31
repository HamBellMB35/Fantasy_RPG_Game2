using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackingState : PlayerBaseState
{
    private AttackData attack;
    
    private bool appliedForceAlaready;

   


   


    private float previousFrameTime;
    public PlayerAttackingState(PlayerStateMachine stateMachine, int attackIndex) : base(stateMachine)
    {
        attack = stateMachine.Attacks[attackIndex];
        // **************** THIS IS JUST TEMPORARY
        // MUST PLACE IN CORRECT SPOT ALTHOUGH WORKING WELL FOR NOW///
        RandomizeAttacks(stateMachine.Attacks);
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(attack.AnimationName, attack.TransitionDuration);

       
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        FaceTarget();

        float normalizedTime = GetNormalizedTime();


        if(normalizedTime >= attack.ForceTime)
        {
            TryApplyForce();

        }

        if (normalizedTime > previousFrameTime && normalizedTime < 1f)
        {
            if(stateMachine.InputReceiver.IsAttacking)              // Check if the player is still attacking
            {
                TryComboAttack(normalizedTime);

            }

        }

        else
        {
            Debug.Log("were back to locomorion");
            // go back to locomotion
        }

        previousFrameTime = normalizedTime;

    }


    public override void Exit()
    {

    }


    private void TryComboAttack(float normalizedTime)
    {
        if (attack.ComboStateIndex == -1) { return; }        // If we cant combo retun

        if (normalizedTime < attack.ComboAttackTime) { return; }     //  If we're not ready to combo attack return

        stateMachine.SwitchState(new PlayerAttackingState(stateMachine, attack.ComboStateIndex));

        
    }

    private void TryApplyForce()
    {
        #region Comments

        // We call this in the tick method when the time has passed force time, meaning
        // when we are far enough trough the animation to apply the force
        #endregion

        if(appliedForceAlaready) { return; }

        stateMachine.ForceReceiver.AddForce(stateMachine.transform.forward * attack.Force);

        appliedForceAlaready = true;

    }


    private float GetNormalizedTime()
    {
        AnimatorStateInfo currentInfo = stateMachine.Animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = stateMachine.Animator.GetNextAnimatorStateInfo(0);

        if(stateMachine.Animator.IsInTransition(0) && nextInfo.IsTag("MeleeAttack"))
        {
            return nextInfo.normalizedTime;
        }

        else if(!stateMachine.Animator.IsInTransition(0) && currentInfo.IsTag("MeleeAttack"))
        {

            return currentInfo.normalizedTime;
        }

        else
        {
            return 0f;
        } 

    }


  


}
