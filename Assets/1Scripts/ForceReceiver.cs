using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    private float verticalVelocity;

    [SerializeField] private CharacterController controller;
    #region Comments
    // A Vector 3 "Movement" multiplied by verical velocity.
    // This will always be happening regardless of what state 
    // were in so that gravity doesnt reset everytime we switch states
    #endregion
    public Vector3 Movement => Vector3.up * verticalVelocity;
    private void Update()
    {
        if(verticalVelocity < 0 && controller.isGrounded)
        {
            verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }

        else
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }
    }
}
