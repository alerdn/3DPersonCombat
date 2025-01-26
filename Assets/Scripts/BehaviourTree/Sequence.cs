using UnityEngine;

[CreateAssetMenu(menuName = "BehaviourTree/Sequence")]
public class Sequence : CompositeNode
{
    public override NodeState OnUpdate(float deltaTime)
    {
        if (currentChild < children.Count)
        {
            switch (children[currentChild].Evaluate(deltaTime, out tree.CurrentNode))
            {
                case NodeState.Running:
                    return NodeState.Running;
                case NodeState.Failure:
                    return NodeState.Failure;
                default:
                    currentChild++;
                    return currentChild == children.Count ? NodeState.Success : NodeState.Running;
            }
        }

        tree.CurrentNode = this;
        return NodeState.Success;
    }
}