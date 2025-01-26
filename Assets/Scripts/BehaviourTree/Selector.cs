using UnityEngine;

[CreateAssetMenu(menuName = "BehaviourTree/Selector")]
public class Selector : CompositeNode
{
    public override NodeState OnUpdate(float deltaTime)
    {
        foreach (Node child in children)
        {
            switch (child.Evaluate(deltaTime, out tree.CurrentNode))
            {
                case NodeState.Running:
                    return NodeState.Running;
                case NodeState.Success:
                    return NodeState.Success;
                default:
                    continue;
            }
        }

        tree.CurrentNode = this;
        return NodeState.Failure;
    }
}