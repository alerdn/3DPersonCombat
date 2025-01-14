using System.Collections.Generic;
using System.Linq;

public class PrioritySelector : Node
{
    protected List<Node> SortedChildren => _sortedChildren ??= SortChildren();

    private List<Node> _sortedChildren;

    public PrioritySelector(string name = "PrioritySelector", int priority = 0) : base(name, priority) { }
    public PrioritySelector(string name, int priority, List<Node> children) : base(name, priority, children) { }

    protected virtual List<Node> SortChildren() => children.OrderByDescending(child => child.Priority).ToList();

    public override void OnStart()
    {
    }

    public override NodeState OnUpdate(float deltaTime)
    {
        foreach (Node child in SortedChildren)
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

    public override void Reset()
    {
        base.Reset();
        _sortedChildren = null;
    }

}