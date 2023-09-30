using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    public PlayerInput.PlayerActions playerActions;

    private PlayerMovement movement;

    private void Awake()
    {
        playerInput = new PlayerInput();
        playerActions = playerInput.Player;

        movement = GetComponent<PlayerMovement>();

    }

    private void FixedUpdate()
    {
        movement.ProcessMove(playerActions.Movement.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        playerActions.Enable();
    }

    private void OnDisable()
    {
        playerActions.Disable();
    }
}
