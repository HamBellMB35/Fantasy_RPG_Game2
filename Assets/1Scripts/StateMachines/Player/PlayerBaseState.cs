using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState : State
#region Initial comments
// This class is used to keep all shared methods between different player states, and a protected reference to the player so that every player state that inherits from this can reference the player if needed
#endregion
{
    protected PlayerStateMachine stateMachine;
 
    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;

    }

    protected void Move( Vector3 motion, float deltaTime)
    {
        stateMachine.Controller.Move((motion + stateMachine.ForceReceiver.Movement) * deltaTime);
    }

    protected void Move( float deltaTime)
    {
        #region Comments
        // This verstion of the move method gets used if we have any states but we dont have any inputs, blocking, or if the player gets hit and still needs to get knockedback, have gravity.
        #endregion

        Move(Vector3.zero, deltaTime); 
    }


    protected void FaceTarget()
    {
        if (stateMachine.Targeter.CurrentTarget == null) { return; }

        #region Comments
        // After making sure we have a target, we get a Vector3 that will give us
        // the vector pointing from the player to the target, and then we cero out the y value
        #endregion

        Vector3 lookAtPos = stateMachine.Targeter.CurrentTarget.transform.position - stateMachine.transform.position;

        lookAtPos.y = 0f;

        stateMachine.transform.rotation = Quaternion.LookRotation(lookAtPos);

    }

    protected void SpawnMagicLIght()
    {
        Debug.Log("A magical light has spawned!");
    }
   
}
