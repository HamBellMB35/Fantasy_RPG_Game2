using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
#region Initial Comments about the class StateMachine
// Even though its not absoloutely necessary we set the State Machine to abstrach since we only want to add subclasses of these( Player state machine, enemy state machine ) to enemy and players
#endregion
{
    private State currentState;

    private void Update()
    {
        #region Comments
        // Checks if currentState isnt null and then calls the Tick method if it isnt.
        //** NOTE: "?" aka Null conditional operator will not work on monobehaviours or scriptable objects afaik**
        #endregion
        currentState?.Tick(Time.deltaTime);
    }

    public void SwitchState(State aNewState)
    {
       
        currentState?.Exit();
        currentState = aNewState;
        #region Comments
        // We check currentState is null just in case we accidentally pass in null(somehow), switch to null, etc
        #endregion
        currentState?.Enter();
    }
}
