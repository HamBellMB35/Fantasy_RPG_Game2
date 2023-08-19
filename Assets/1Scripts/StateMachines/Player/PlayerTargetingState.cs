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
}
