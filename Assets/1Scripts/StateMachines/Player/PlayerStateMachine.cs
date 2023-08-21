using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [field: SerializeField] public InputReceiver InputReceiver { get; private set; }
    [field: SerializeField] public CharacterController Controller { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public Targeter Targeter{ get; private set; }
    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
    [field: SerializeField] public float FreeLookMovementSpeed { get; private set; }
    [field: SerializeField] public float RotationSmoothingValue { get; private set; }
    public Transform MainCameraTransform { get; private set; }

    private void Start()
    {
        #region Comments
        // We grab the main camera transform to set the orientation of our player based on the position of the camera
        // This is done here so any of our states can access it
        // Then we inmediately switch to the PlayerFreelook state in start.
        #endregion 

        MainCameraTransform = Camera.main.transform;
        SwitchState(new PlayerFreeLookState(this));                             
    }


  
}
