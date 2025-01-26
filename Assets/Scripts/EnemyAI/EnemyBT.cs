using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBT : BehaviourTree
{
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public CharacterController CharacterController { get; private set; }
    [field: SerializeField] public NavMeshAgent Agent { get; private set; }
    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
    [field: SerializeField] public MeleeWeapon Weapon { get; private set; }
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public HealthUI HealthUI { get; private set; }
    [field: SerializeField] public Target Target { get; private set; }
    [field: SerializeField] public Ragdoll Ragdoll { get; private set; }
    [field: SerializeField] public Transform[] Waypoints { get; set; }

    [field: SerializeField] public float MovementSpeed { get; private set; }
    [field: SerializeField] public float RetreatSpeed { get; private set; }
    [field: SerializeField] public float PlayerChasingRange { get; private set; } = 4f;
    [field: SerializeField] public float AttackRange { get; private set; } = 2f;
    [field: SerializeField] public int SoulsValue;
    [field: SerializeField] public bool CanBeStunned;

    public PlayerStateMachine Player { get; private set; }

    public bool HasNoticedPlayer;

    private Vector3 _initialPosition;

    private void Start()
    {
        Player = PlayerStateMachine.Instance;

        Root = SetupTree();

        Agent.updatePosition = false;
        Agent.updateRotation = false;

        _initialPosition = transform.position;
    }

    protected override Node SetupTree()
    {
        Node root = Root.Instantiate();

        root.SetBehaviourTree(this);

        return root;
    }


    private void HandleTakeDamage()
    {
        if (CanBeStunned)
        {
            // Stun
        }
    }

    private void HandleDie()
    {
        // Die
    }

    public void Respawn()
    {
        Ragdoll.ToggleRagdoll(false);
        Weapon.gameObject.SetActive(true);
        Target.enabled = true;

        HasNoticedPlayer = false;
        HealthUI.gameObject.SetActive(true);

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
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, PlayerChasingRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackRange);
    }
}