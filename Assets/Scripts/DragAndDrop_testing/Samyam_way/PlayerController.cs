using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerControls _playerControls;

    private void Awake()
    {
        _playerControls = new PlayerControls(); 
    }

    private void OnEnable()
    {
        _playerControls.Enable();
        _playerControls.Land.Jump.performed += Jump;
    }

    private void OnDisable()
    {
        _playerControls.Disable();
        _playerControls.Land.Jump.performed += Jump;
    }

    private void Update()
    {
        Vector2 move = _playerControls.Land.Move.ReadValue<Vector2>();

        if (_playerControls.Land.Jump.triggered)
        {
            Debug.Log("Jump");
        }
    }

    private void Jump(InputAction.CallbackContext context)
    {
        Debug.Log("Jump");
    }
}
