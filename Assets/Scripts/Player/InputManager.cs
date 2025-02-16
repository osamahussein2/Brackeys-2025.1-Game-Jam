using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    private PlayerInputActions playerInput;
    private PlayerInputActions.PlayingActionsActions playingActions;
    private PlayerMove playerMove;

    public static InputManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        playerInput = new PlayerInputActions();
        playingActions = playerInput.PlayingActions;
        playingActions.Enable();
        playerMove = GetComponent<PlayerMove>();

        playingActions.Dash.started += ctx => playerMove.Dash(playingActions.Movement.ReadValue<Vector2>());
    }

    private void Update()
    {
        playerMove.ProccessMove(playingActions.Movement.ReadValue<Vector2>(), playingActions.Sprint.IsPressed());
    }

    ///onFootActions.Crouch.canceled += ctx => crouch.EndAbility();
}
