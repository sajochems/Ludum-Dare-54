using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private InputManager input;
    private Rigidbody2D rb;
    public float moveSpeed = 1.5f;

    private void Awake()
    {

        rb = GetComponent<Rigidbody2D>();
    }

    public void ProcessMove(Vector2 input)
        {
            rb.velocity = input * moveSpeed;
        }

    /*private void OnEnable()
    {
        input.playerActions.Movement.performed += OnMovementPerformed;
        input.playerActions.Movement.canceled += OnMovementCancelled;
    }*/

   /* private void OnDisable()
    {
        input.playerActions.Movement.performed -= OnMovementPerformed;
        input.playerActions.Movement.canceled -= OnMovementCancelled;
    }*/

    
}
