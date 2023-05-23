using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : Controls.IGamesActions
{
    private Controls _controls;
    public Action<Vector2> OnMoveEvent;

    public InputReader()
    {
        _controls = new Controls();
        _controls.Games.SetCallbacks(this);
        _controls.Games.Enable();
    }
    
    public void OnMove(InputAction.CallbackContext context)
    {
        OnMoveEvent?.Invoke(context.ReadValue<Vector2>());
    }
}
