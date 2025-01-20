using UnityEngine;

[CreateAssetMenu(menuName = "BehaviourTree/Checks/CheckIsDead")]
public class CheckIsDead : Node
{
    public override void OnStart() { }

    public override NodeState OnUpdate(float deltaTime)
    {
        if (tree.Health.IsDead)
        {
            return NodeState.Success;
        }

        return NodeState.Failure;
    }

    public override void OnStop() { }
}