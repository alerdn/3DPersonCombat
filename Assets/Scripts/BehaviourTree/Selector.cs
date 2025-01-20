using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BehaviourTree/Selector")]
public class Selector : CompositeNode
{
    public override void OnStart()
    {
    }

    public override NodeState OnUpdate(float deltaTime)
    {
        foreach (Node child in children)
        {
            switch (child.Evaluate(deltaTime))
            {
                case NodeState.Running:
                    return NodeState.Running;
                case NodeState.Success:
                    Reset();
                    return NodeState.Success;
                default:
                    continue;
            }
        }

        Reset();
        return NodeState.Failure;
    }

    public override void OnStop()
    {
    }
}