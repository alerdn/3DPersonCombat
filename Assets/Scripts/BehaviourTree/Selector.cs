using System.Collections.Generic;

public class Selector : Node
{
    public Selector() : base() { }
    public Selector(string name, int priority, List<Node> children) : base(name, priority, children) { }

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
                case NodeState.Success:
                    Reset();
                    return NodeState.Success;
                default:
                    currentChild++;
                    return NodeState.Running;
            }
        }

        Reset();
        return NodeState.Failure;
    }

    public override void OnStop()
    {
    }
}