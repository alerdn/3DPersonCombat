using UnityEngine;

[CreateAssetMenu(menuName = "BehaviourTree/Checks/CheckPlayerInAttackRange")]
public class CheckPlayerInAttackRange : Node
{
    public override void OnStart() { }

    public override NodeState OnUpdate(float deltaTime)
    {
        if (tree.Player.Health.IsDead) return NodeState.Failure;

        float playerDistanceSqr = (tree.Player.transform.position - tree.transform.position).sqrMagnitude;
        float attackRangeSqr = Mathf.Pow(tree.AttackRange, 2f);

        if (playerDistanceSqr <= attackRangeSqr)
        {
            return NodeState.Success;
        }

        return NodeState.Failure;
    }

    public override void OnStop() { }
}