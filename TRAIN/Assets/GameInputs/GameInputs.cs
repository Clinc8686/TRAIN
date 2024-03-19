using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInputs : MonoBehaviour
{
    public static GameInputs Instance { get; private set; }

    public event EventHandler OnPlayerUsedLeftMouseButton;

    private PlayerInputActions _playerInputActions;
    private void OnEnable()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Enable();
    }
    private void OnDisable()
    {
        _playerInputActions.Disable();
    }
    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    private void Start()
    {
        _playerInputActions.Player.MouseMoveAndInteract.performed += InputSystem_Player_MouseMoveAndInteract;
    }
    private void InputSystem_Player_MouseMoveAndInteract(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPlayerUsedLeftMouseButton?.Invoke(this, EventArgs.Empty);
    }
}
