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

}
