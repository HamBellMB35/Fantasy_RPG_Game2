using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{
    private readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");

    private const float AnimatorDampTime = 0.1f;

    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {

    }

    public override void Tick(float deltaTime)
    {
        Vector3 movement = CalculateEntityMovement();



        stateMachine.Controller.Move(movement * stateMachine.FreeLookMovementSpeed * deltaTime);

        if (stateMachine.InputReceiver.MovementValue == Vector2.zero)
        {
            stateMachine.Animator.SetFloat(FreeLookSpeedHash, 0, AnimatorDampTime, deltaTime);
            return;
        }


        stateMachine.Animator.SetFloat(FreeLookSpeedHash, 1, AnimatorDampTime, deltaTime);
        FaceMovementDirection(movement, deltaTime);

        Debug.Log(stateMachine.InputReceiver.MovementValue);

    }


    public override void Exit()
    {
      

    }

    private Vector3 CalculateEntityMovement()
    {
        Vector3 forward = stateMachine.MainCameraTransform.forward;
        Vector3 right = stateMachine.MainCameraTransform.right;

        forward.y = 0;
        forward.Normalize();

        right.y = 0;
        right.Normalize();

        #region Comments

        // forward and right are relative to where the camera forward and camera right are
        // stateMachine.InputReceiver.MovementValue.y is us pressing W and S
        // stateMachine.InputReceiver.MovementValue.x is us pressing D and A

        #endregion

        return forward * stateMachine.InputReceiver.MovementValue.y +
            right * stateMachine.InputReceiver.MovementValue.x;
    }


    private void FaceMovementDirection(Vector3 movement, float deltaTime)
    {
        stateMachine.transform.rotation = Quaternion.Lerp(
            stateMachine.transform.rotation, Quaternion.LookRotation(movement),
            deltaTime * stateMachine.RotationSmoothingValue);
    
           
    }

}
