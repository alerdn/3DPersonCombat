using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static Controls;

[CreateAssetMenu(fileName = "InputReader")]
public class InputReader : ScriptableObject, IPlayerActions
{
    public event Action JumpEvent;
    public event Action DodgeEvent;
    public event Action ToggleTargetEvent;
    public event Action SwitchWeaponEvent;
    public event Action SwitchSecondaryWeaponEvent;

    public bool IsAttacking { get; private set; }
    public bool IsBlocking { get; private set; }
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

    public void OnLook(InputAction.CallbackContext context) { }

    public void OnToggleTarget(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        ToggleTargetEvent?.Invoke();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsAttacking = true;
        }
        else if (context.canceled)
        {
            IsAttacking = false;
        }
    }

    public void OnBlock(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsBlocking = true;
        }
        else if (context.canceled)
        {
            IsBlocking = false;
        }
    }

    public void OnSwitchWeapon(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        SwitchWeaponEvent?.Invoke();
    }

    public void OnSwitchSecondaryWeapon(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        SwitchSecondaryWeaponEvent?.Invoke();
    }
}
