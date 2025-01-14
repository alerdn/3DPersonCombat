using System.Collections.Generic;

public class Sequence : Node
{
    public Sequence(string name = "Sequence", int priority = 0) : base(name, priority) { }
    public Sequence(string name, int priority, List<Node> children) : base(name, priority, children) { }

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