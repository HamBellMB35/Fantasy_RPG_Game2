using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    private readonly int TargetingBlendTreeHash = Animator.StringToHash("TargetingBlendTree");

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

        FaceTarget();

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
