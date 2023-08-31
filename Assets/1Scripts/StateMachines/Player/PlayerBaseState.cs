using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Linq;
using UnityEngine;

public abstract class PlayerBaseState : State
#region Initial comments
// This class is used to keep all shared methods between different player states, and a protected reference to the player so that every player state that inherits from this can reference the player if needed
#endregion
{
    protected PlayerStateMachine stateMachine;

    private AttackData[] swordAttackList;

    private int attackListLength;

    private static System.Random _random = new System.Random();

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        swordAttackList = stateMachine.Attacks;
        attackListLength = swordAttackList.Length;
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
    /// <summary>
    /// ********************FIX THIS*************
    /// </summary>
    /// <param name="attackDataArray"></param>
    public void RandomizeAttacks(AttackData[] attackDataArray) // Maybe move to playerbaseState 
    {
        int lastIndex = attackDataArray.Length - 1;

        for (int currentAttackIndex = attackListLength - 2; currentAttackIndex > 0; currentAttackIndex--)
        {
            int randomIndex = _random.Next(0, currentAttackIndex + 1);

            int temp = attackDataArray[randomIndex].ComboStateIndex;

            attackDataArray[randomIndex].ComboStateIndex = attackDataArray[currentAttackIndex].ComboStateIndex;

            attackDataArray[currentAttackIndex].ComboStateIndex = temp;
        }

        attackDataArray[lastIndex].ComboStateIndex = -1;
    }

}
