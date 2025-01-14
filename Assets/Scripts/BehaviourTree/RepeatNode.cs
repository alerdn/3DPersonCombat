public class RepeatNode : DecoratorNode
{
    private int _count;

    public RepeatNode(Node child, int count) : base(child)
    {
        _count = count;
    }

    public override void OnStart()
    {
    }

    public override NodeState OnUpdate(float deltaTime)
    {
        for (int i = 0; i < _count; i++)
        {
            child.Evaluate(deltaTime);
        }

        return NodeState.Success;
    }

    public override void OnStop()
    {
    }
}