using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReader")]
public class InputReader : ScriptableObject, Controls.IPlayerActions
{
    public event Action JumpEvent;
    public event Action DodgeEvent;

    public Vector2 MovementValue { get; private set; }

    private Controls _constrols;

    private void OnEnable()
    {
        _constrols = new Controls();
        _constrols.Player.SetCallbacks(this);
        _constrols.Player.Enable();
    }

    private void OnDisable()
    {
        _constrols.Player.Disable();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        JumpEvent?.Invoke();
    }

    public void OnDodge(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        DodgeEvent?.Invoke();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
    }
}
