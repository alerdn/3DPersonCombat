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

        float playerDistanceSqr = (tree.Player.transform.position - tree.transform.position).sqrMagnitude;

        if (playerDistanceSqr <= Mathf.Pow(tree.PlayerChasingRange, 2f))
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