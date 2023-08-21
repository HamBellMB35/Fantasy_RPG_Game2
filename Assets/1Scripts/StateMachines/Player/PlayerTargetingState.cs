using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    private readonly int TargetingBlendTreeHash = Animator.StringToHash("TargetingBlendTree");
    private readonly int TargetingForwardHash = Animator.StringToHash("TargetingForwardSpeed");
    private readonly int TargetingRightHash = Animator.StringToHash("TargetingRightSpeed");

    public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine) { }
  

    public override void Enter()
    {
        stateMachine.InputReceiver.CancelTargetEvent += OnCancel;
        stateMachine.Animator.Play(TargetingBlendTreeHash);

    }

    public override void Tick(float deltaTime)
    {
       if(stateMachine.Targeter.CurrentTarget == null)
       {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            return;
       }

        Vector3 movement = CalculateMovement();

        Move( movement * stateMachine.TargetingMovementSpeed, deltaTime );

        UdapteAnimator(deltaTime);

        FaceTarget();

    }

    private void UdapteAnimator(float deltaTime)
    {
        if(stateMachine.InputReceiver.MovementValue.y == 0)
        {
            stateMachine.Animator.SetFloat(TargetingForwardHash, 0, 0.1f, deltaTime);
        }

        else
        {
            float value = stateMachine.InputReceiver.MovementValue.y > 0 ? 1f : -1f;
            stateMachine.Animator.SetFloat(TargetingForwardHash, value, 0.1f, deltaTime);

        }

        if (stateMachine.InputReceiver.MovementValue.x == 0)
        {
            stateMachine.Animator.SetFloat(TargetingRightHash, 0, 0.1f, deltaTime) ;
        }

        else
        {
            float value = stateMachine.InputReceiver.MovementValue.x > 0 ? 1f : -1f;
            stateMachine.Animator.SetFloat(TargetingRightHash, value, 0.1f, deltaTime);

        }
    } 

    public override void Exit()
    {
        stateMachine.InputReceiver.CancelTargetEvent -= OnCancel;
    }

    private void OnCancel()
    {
        stateMachine.Targeter.CancelTargeting();
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }

    private Vector3 CalculateMovement()
    {

        #region Comments
        // This method will make the player move in a circular manner around the target
        #endregion

        Vector3 movement = new Vector3();

        #region Comments
        // Because the player is facing the target we can get the player's right vector and
        // multiplay that by the input, first sideways (x axis) then forwards and backwards
        // (y axis)
        #endregion

        movement += stateMachine.transform.right * stateMachine.InputReceiver.MovementValue.x;
        movement += stateMachine.transform.forward * stateMachine.InputReceiver.MovementValue.y;

        return movement;
    }
}
