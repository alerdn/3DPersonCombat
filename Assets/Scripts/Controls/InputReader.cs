using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static Controls;

public enum ControllerMode
{
    Gameplay,
    UI
}

[CreateAssetMenu(fileName = "InputReader")]
public class InputReader : ScriptableObject, IPlayerActions
{
    public event Action JumpEvent;
    public event Action DodgeEvent;
    public event Action ToggleTargetEvent;
    public event Action SwitchWeaponEvent;
    public event Action SwitchSecondaryWeaponEvent;
    public event Action UseItemEvent;
    public event Action SwitchItemEvent;
    public event Action InteractEvent;

    public event Action IncreaseStrengthEvent;
    public event Action IncreaseVigorEvent;
    public event Action IncreaseFortitudeEvent;

    public bool IsAttacking { get; private set; }
    public bool IsBlocking { get; private set; }
    public Vector2 MovementValue { get; private set; }

    private Controls _constrols;
    private ControllerMode _controllerMode;

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

    public void SetControllerMode(ControllerMode mode)
    {
        _controllerMode = mode;
        switch (_controllerMode)
        {
            case ControllerMode.Gameplay:
                _constrols.Player.Enable();
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                break;
            case ControllerMode.UI:
                _constrols.Player.Disable();
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                break;
        }
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

    public void OnLook(InputAction.CallbackContext context)
    {
        if (_controllerMode == ControllerMode.UI) return;
    }

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

    public void OnUseItem(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        UseItemEvent?.Invoke();
    }

    public void OnSwitchItem(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        SwitchItemEvent?.Invoke();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        InteractEvent?.Invoke();
    }

    public void OnIncreaseStrength(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        IncreaseStrengthEvent?.Invoke();
    }

    public void OnIncreaseVigor(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        IncreaseVigorEvent?.Invoke();
    }

    public void OnIncreaseFortitude(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        IncreaseFortitudeEvent?.Invoke();
    }
}
