using UnityEngine;

public class CheckPlayerInRange : EnemyNodeBase
{
    public CheckPlayerInRange(EnemyBT tree) : base(tree)
    {
    }

    public override void OnStart()
    {
    }

    public override NodeState OnUpdate(float deltaTime)
    {
        if (tree.Player.Health.IsDead) return NodeState.Failure;

        if (tree.HasNoticedPlayer) return NodeState.Success;

        float playerDistanceSqr = (tree.Player.transform.position - tree.transform.position).sqrMagnitude;

        if (playerDistanceSqr <= tree.PlayerChasingRange * tree.PlayerChasingRange)
        {
            tree.HasNoticedPlayer = true;
            return NodeState.Success;
        }

        return NodeState.Failure;
    }

    public override void OnStop()
    {
    }
}