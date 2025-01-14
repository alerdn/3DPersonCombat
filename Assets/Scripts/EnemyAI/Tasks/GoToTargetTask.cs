using UnityEngine;

public class GoToTargetTask : EnemyNodeBase
{
    private readonly int LocomotionHash = Animator.StringToHash("Locomotion");
    private readonly int SpeedHash = Animator.StringToHash("Speed");

    public GoToTargetTask(EnemyBT tree) : base(tree)
    {
    }

    public override void OnStart()
    {
        tree.Animator.CrossFadeInFixedTime(LocomotionHash, .1f);
    }

    public override NodeState OnUpdate(float deltaTime)
    {
        if (tree.Agent.isOnNavMesh)
        {
            tree.Agent.destination = tree.Player.transform.position;
            Move(tree.Agent.desiredVelocity.normalized * tree.MovementSpeed, deltaTime);
        }

        // Atualizamos a velocity do agent porque nós estamos lidando com a movimentação manualmente
        tree.Agent.velocity = tree.CharacterController.velocity;

        FacePlayer();
        tree.Animator.SetFloat(SpeedHash, 1f, .1f, deltaTime);

        return NodeState.Running;
    }

    public override void OnStop()
    {
        if (tree.Agent.isOnNavMesh)
        {
            tree.Agent.ResetPath();
        }
        tree.Agent.velocity = Vector3.zero;
    }
}