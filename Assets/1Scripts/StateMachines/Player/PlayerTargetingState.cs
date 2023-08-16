using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine) { }
  

    public override void Enter()
    {
        stateMachine.InputReceiver.CancelTargetEvent += OnCancel;
    }

    public override void Tick(float deltaTime)
    {
        
    }

    public override void Exit()
    {
        stateMachine.InputReceiver.CancelTargetEvent -= OnCancel;
    }

    private void OnCancel()
    {
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }
}
