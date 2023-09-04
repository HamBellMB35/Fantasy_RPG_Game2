using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class ForceReceiver : MonoBehaviour
{
    [SerializeField] private CharacterController controller;

    [SerializeField] private float entityDrag = 0.1f;
    
    public float verticalVelocity;

    private Vector3 impact;

    private Vector3 dampingVelocity;
    #region Comments
    // A Vector 3 "Movement" multiplied by verical velocity.
    // This will always be happening regardless of what state 
    // were in so that gravity doesnt reset everytime we switch states
    #endregion
    public Vector3 Movement => impact + Vector3.up * verticalVelocity;
    private void Update()
    {
        if( verticalVelocity < 0f && controller.isGrounded)
        {
            verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }

        else
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }

        impact = Vector3.SmoothDamp(impact, Vector3.zero, ref dampingVelocity, entityDrag);

    }

    public void AddForce(Vector3 force)
    {
        // How much for you want to add and in what direction
        impact += force;
    }
}
