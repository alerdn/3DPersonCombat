using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BehaviourTree/Sequence")]
public class Sequence : CompositeNode
{
    public override void OnStart()
    {
    }

    public override NodeState OnUpdate(float deltaTime)
    {
        if (currentChild < children.Count)
        {
            switch (children[currentChild].Evaluate(deltaTime))
            {
                case NodeState.Running:
                    return NodeState.Running;
                case NodeState.Failure:
                    Reset();
                    return NodeState.Failure;
                default:
                    currentChild++;
                    return currentChild == children.Count ? NodeState.Success : NodeState.Running;
            }
        }

        Reset();
        return NodeState.Success;
    }

    public override void OnStop()
    {
    }
}