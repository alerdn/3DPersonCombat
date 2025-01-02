using System;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public static PlayerStateMachine Instance;

    [field: SerializeField] public InputReader InputReader { get; private set; }
    [field: SerializeField] public CharacterController CharacterController { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public Targeter Targeter { get; private set; }
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public Stamina Stamina { get; private set; }
    [field: SerializeField] public Ragdoll Ragdoll { get; private set; }
    [field: SerializeField] public LedgeDetector LedgeDetector { get; private set; }
    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
    [field: SerializeField] public Inventory Inventory { get; private set; }
    [field: SerializeField] public float FreeLookMovementSpeed { get; private set; }
    [field: SerializeField] public float BlockingMovementSpeed { get; private set; }
    [field: SerializeField] public float RotationDamping { get; private set; }
    [field: SerializeField] public float DodgeDuration { get; private set; }
    [field: SerializeField] public float DodgeLength { get; private set; }
    [field: SerializeField] public int DodgeStaminaCost { get; internal set; }
    [field: SerializeField] public float JumpForce { get; private set; }
    [field: SerializeField] public GameObject EstusFlask { get; private set; }
    [field: SerializeField] public SoulCollectableItem SoulDropPrefab { get; private set; }
    [field: SerializeField] public Weapon[] PrimaryWeapons { get; private set; }
    [field: SerializeField] public Weapon[] SecondaryWeapons { get; private set; }

    public Weapon CurrentWeapon => CurrentPrimaryWeapon;

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

    private void OnEnable()
    {
        Health.OnTakeDamage += HandleTakeDamage;
        Health.OnDie += HandleDie;
    }

    private void OnDisable()
    {
        Health.OnTakeDamage -= HandleTakeDamage;
        Health.OnDie -= HandleDie;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        MainCameraTransform = Camera.main.transform;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        SwitchState(new PlayerFreeLookState(this));
        SwitchPrimaryWeapon();
        SwitchSecondaryWeapon();

        Health.SetStaminaComponent(Stamina);
        Inventory.ReplanishHealItem();
    }

    public void SwitchPrimaryWeapon()
    {
        CurrentPrimaryWeapon?.gameObject.SetActive(false);
        _currentPrimaryWeaponIndex = (_currentPrimaryWeaponIndex + 1) % PrimaryWeapons.Length;
        CurrentPrimaryWeapon?.gameObject.SetActive(true);
    }

    public void SwitchSecondaryWeapon()
    {
        CurrentSecondaryWeapon?.gameObject.SetActive(false);
        _currentSecondaryWeaponIndex = (_currentSecondaryWeaponIndex + 1) % SecondaryWeapons.Length;
        CurrentSecondaryWeapon?.gameObject.SetActive(true);
    }

    private void HandleTakeDamage()
    {
        SwitchState(new PlayerImpactState(this));
    }

    private void HandleDie()
    {
        SwitchState(new PlayerDeadState(this));
    }

    public void DropSouls()
    {
        Instantiate(SoulDropPrefab, transform.position, Quaternion.identity).Init(Inventory.Souls);
        Inventory.Souls = 0;
    }

    public void Respawn()
    {
        Ragdoll.ToggleRagdoll(false);
        CurrentWeapon.gameObject.SetActive(true);

        Inventory.ReplanishHealItem();
        Health.RestoreHealth();

        SetPosition(CheckpointManager.Instance.GetLastCheckpoint());

        SwitchState(new PlayerFreeLookState(this));
    }

    public void SetPosition(Vector3 position)
    {
        CharacterController.enabled = false;
        transform.position = position;
        CharacterController.enabled = true;
    }
}
