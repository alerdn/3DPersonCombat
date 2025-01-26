using UnityEngine;

[CreateAssetMenu(menuName = "BehaviourTree/Checks/CheckPlayerInRange")]
public class CheckPlayerInRange : Node
{
    public override void OnStart()
    {
    }

    public override NodeState OnUpdate(float deltaTime)
    {
        if (tree.Player.Health.IsDead) return NodeState.Failure;

        if (tree.HasNoticedPlayer) return NodeState.Success;

        float playerDistanceSqr = GetDistanceSqrToPlayer();
        float playerChasingRangeSqr = Mathf.Pow(tree.PlayerChasingRange, 2f);

        if (playerDistanceSqr <= playerChasingRangeSqr)
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