using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [field: SerializeField] public InputReader InputReader { get; private set; }
    [field: SerializeField] public CharacterController CharacterController { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public Targeter Targeter { get; private set; }
    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
    [field: SerializeField] public float FreeLookMovementSpeed { get; private set; }
    [field: SerializeField] public float RotationDamping { get; private set; }
    [field: SerializeField] public Weapon[] PrimaryWeapons { get; private set; }
    [field: SerializeField] public Weapon[] SecondaryWeapons { get; private set; }

    public Weapon CurrentWeapon
    {
        get
        {
            if (CurrentSecondaryWeapon?.IsTwoHanded == true)
            {
                return CurrentSecondaryWeapon;
            }
            else
            {
                return CurrentPrimaryWeapon;
            }
        }
    }

    public Weapon CurrentPrimaryWeapon
    {
        get
        {
            if (_currentPrimaryWeaponIndex <= -1) return null;
            return PrimaryWeapons[_currentPrimaryWeaponIndex];
        }
    }
    public Weapon CurrentSecondaryWeapon
    {
        get
        {
            if (_currentSecondaryWeaponIndex <= -1) return null;
            return SecondaryWeapons[_currentSecondaryWeaponIndex];
        }
    }

    public Transform MainCameraTransform { get; private set; }

    private int _currentPrimaryWeaponIndex = -1;
    private int _currentSecondaryWeaponIndex = -1;

    private void Start()
    {
        MainCameraTransform = Camera.main.transform;

        Cursor.lockState = CursorLockMode.Locked;

        SwitchState(new PlayerFreeLookState(this));
        SwitchPrimaryWeapon();
        SwitchSecondaryWeapon();
    }

    public void SwitchPrimaryWeapon()
    {
        if (_currentPrimaryWeaponIndex < -1)
        {
            _currentPrimaryWeaponIndex += 9;
        }

        CurrentPrimaryWeapon?.gameObject.SetActive(false);
        _currentPrimaryWeaponIndex = (_currentPrimaryWeaponIndex + 1) % PrimaryWeapons.Length;
        CurrentPrimaryWeapon?.gameObject.SetActive(true);

        if (CurrentPrimaryWeapon?.IsTwoHanded == true || CurrentSecondaryWeapon?.IsTwoHanded == true)
        {
            CurrentSecondaryWeapon?.gameObject.SetActive(false);
            _currentSecondaryWeaponIndex -= 10;
        }
        else if (_currentSecondaryWeaponIndex < -1)
        {
            SwitchSecondaryWeapon();
        }
    }

    public void SwitchSecondaryWeapon()
    {
        if (_currentSecondaryWeaponIndex < -1)
        {
            _currentSecondaryWeaponIndex += 9;
        }

        CurrentSecondaryWeapon?.gameObject.SetActive(false);
        _currentSecondaryWeaponIndex = (_currentSecondaryWeaponIndex + 1) % SecondaryWeapons.Length;
        CurrentSecondaryWeapon?.gameObject.SetActive(true);

        if (CurrentSecondaryWeapon?.IsTwoHanded == true || CurrentPrimaryWeapon?.IsTwoHanded == true)
        {
            CurrentPrimaryWeapon?.gameObject.SetActive(false);
            _currentPrimaryWeaponIndex -= 10;
        }
        else if (_currentPrimaryWeaponIndex < -1)
        {
            SwitchPrimaryWeapon();
        }
    }
}
