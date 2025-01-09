using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : StateMachine
{

    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public CharacterController CharacterController { get; private set; }
    [field: SerializeField] public NavMeshAgent Agent { get; private set; }
    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
    [field: SerializeField] public MeleeWeapon Weapon { get; private set; }
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public Target Target { get; private set; }
    [field: SerializeField] public Ragdoll Ragdoll { get; private set; }

    [field: SerializeField] public float MovementSpeed { get; private set; }
    [field: SerializeField] public float PlayerChasingRange { get; private set; } = 10f;
    [field: SerializeField] public float AttackRange { get; private set; } = 2f;
    [field: SerializeField] public int SoulsValue;

    public PlayerStateMachine Player { get; private set; }

    private Vector3 _initialPosition;

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

    private void Start()
    {
        Player = PlayerStateMachine.Instance;

        Agent.updatePosition = false;
        Agent.updateRotation = false;

        SwitchState(new EnemyIdleState(this));
        _initialPosition = transform.position;
    }

    private void HandleTakeDamage()
    {
        SwitchState(new EnemyImpactState(this));
    }

    private void HandleDie()
    {
        SwitchState(new EnemyDeadState(this));
    }

    public void Respawn()
    {
        Ragdoll.ToggleRagdoll(false);
        Weapon.gameObject.SetActive(true);
        Target.enabled = true;

        Health.RestoreHealth();

        // Resetando posição
        CharacterController.enabled = false;
        transform.position = _initialPosition;
        CharacterController.enabled = true;

        // Resetando agent
        Agent.enabled = false;
        Agent.enabled = true;
        if (Agent.isOnNavMesh)
        {
            Agent.ResetPath();
        }
        Agent.velocity = Vector3.zero;

        SwitchState(new EnemyIdleState(this));
    }
}
