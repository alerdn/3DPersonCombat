using System;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public static PlayerStateMachine Instance;

    public event Action<Weapon> OnWeaponSwitched;
    public event Action<Shield> OnShieldSwitched;
    public event Action<ItemInventory> OnItemSwitched;
    public event Action<Spell> OnSpellSwitched;

    [field: SerializeField] public InputReader InputReader { get; private set; }
    [field: SerializeField] public CharacterController CharacterController { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public Targeter Targeter { get; private set; }
    [field: SerializeField] public CharacterStat CharacterStat { get; private set; }
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public Mana Mana { get; private set; }
    [field: SerializeField] public Stamina Stamina { get; private set; }
    [field: SerializeField] public Ragdoll Ragdoll { get; private set; }
    [field: SerializeField] public LedgeDetector LedgeDetector { get; private set; }
    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
    [field: SerializeField] public Inventory Inventory { get; private set; }
    [field: SerializeField] public Spellbook Spellbook { get; private set; }
    [field: SerializeField] public float FreeLookMovementSpeed { get; private set; }
    [field: SerializeField] public float BlockingMovementSpeed { get; private set; }
    [field: SerializeField] public float RotationDamping { get; private set; }
    [field: SerializeField] public float DodgeDuration { get; private set; }
    [field: SerializeField] public float DodgeLength { get; private set; }
    [field: SerializeField] public int DodgeStaminaCost { get; internal set; }
    [field: SerializeField] public float JumpForce { get; private set; }
    [field: SerializeField] public GameObject EstusFlask { get; private set; }
    [field: SerializeField] public SoulCollectableItem SoulDropPrefab { get; private set; }
    [field: SerializeField] public Weapon[] Weapons { get; private set; }
    [field: SerializeField] public Shield[] Shields { get; private set; }

    public Weapon CurrentWeapon
    {
        get
        {
            if (_currentWeaponIndex <= -1) return null;
            return Weapons[_currentWeaponIndex];
        }
    }
    public Shield CurrentShield
    {
        get
        {
            if (_currentShieldIndex <= -1) return null;
            return Shields[_currentShieldIndex];
        }
    }

    public Transform MainCameraTransform { get; private set; }

    private int _currentWeaponIndex = -1;
    private int _currentShieldIndex = -1;
    private SoulCollectableItem _lastSoulsDropped;

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
        InputReader.SetControllerMode(ControllerMode.Gameplay);

        SwitchState(new PlayerFreeLookState(this));
        SwitchWeapon();
        SwitchShield();

        Health.SetStaminaComponent(Stamina);
        Inventory.ReplanishHealItem();

        Health.SetMaxHealth(CharacterStat.Vigor, true);
        Stamina.SetMaxStamina(CharacterStat.Endurance, true);
        Mana.SetMaxMana(CharacterStat.Mind, true);
    }

    public void SwitchWeapon()
    {
        CurrentWeapon?.gameObject.SetActive(false);
        _currentWeaponIndex = (_currentWeaponIndex + 1) % Weapons.Length;
        CurrentWeapon?.gameObject.SetActive(true);

        OnWeaponSwitched?.Invoke(CurrentWeapon);
    }

    public void SwitchShield()
    {
        CurrentShield?.gameObject.SetActive(false);
        _currentShieldIndex = (_currentShieldIndex + 1) % Shields.Length;
        CurrentShield?.gameObject.SetActive(true);

        OnShieldSwitched?.Invoke(CurrentShield);
    }

    public void SwitchItem()
    {
        Inventory.SwitchItem();
        OnItemSwitched?.Invoke(Inventory.CurrentItem);
    }

    public void SwitchSpell()
    {
        Spellbook.SwitchSpell();
        OnSpellSwitched?.Invoke(Spellbook.CurrentSpell);
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
        if (_lastSoulsDropped != null)
        {
            Destroy(_lastSoulsDropped.gameObject);
        }

        _lastSoulsDropped = Instantiate(SoulDropPrefab, transform.position, Quaternion.identity);
        _lastSoulsDropped.Init(Inventory.Souls);

        Inventory.Souls = 0;
    }

    public void Respawn()
    {
        Ragdoll.ToggleRagdoll(false);
        CurrentWeapon.gameObject.SetActive(true);

        Inventory.ReplanishHealItem();
        RestoreStats();

        SetPosition(CheckpointManager.Instance.GetLastCheckpoint());

        SwitchState(new PlayerFreeLookState(this));

        EnemyManager.Instance.RespawnAll();
    }

    public void RestoreStats()
    {
        Health.RestoreHealth();
        Stamina.RestoreStamina();
        Mana.RestoreMana();
    }

    public void SetPosition(Vector3 position)
    {
        CharacterController.enabled = false;
        transform.position = position;
        CharacterController.enabled = true;
    }

    public void SetVigor(int nextVigor)
    {
        CharacterStat.Vigor = nextVigor;
        Health.SetMaxHealth(CharacterStat.Vigor, true);
    }

    public void SetEndurance(int nextEndurance)
    {
        CharacterStat.Endurance = nextEndurance;
        Stamina.SetMaxStamina(CharacterStat.Endurance, true);
    }

    public void SetMind(int nextMind)
    {
        CharacterStat.Mind = nextMind;
        Mana.SetMaxMana(CharacterStat.Mind, true);
    }

    public void SetStrength(int nextStrength)
    {
        CharacterStat.Strength = nextStrength;
    }

    public void SetIntelligence(int nextIntelligence)
    {
        CharacterStat.Intelligence = nextIntelligence;
    }
}
