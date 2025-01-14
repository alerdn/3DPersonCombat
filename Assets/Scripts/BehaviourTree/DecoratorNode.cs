public abstract class DecoratorNode : Node
{
    protected Node child;

    public DecoratorNode(Node child)
    {
        this.child = child;
    }
}