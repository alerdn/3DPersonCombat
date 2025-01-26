using System.Collections.Generic;
using UnityEngine;

public abstract class CompositeNode : Node
{
    [SerializeField] protected List<Node> children = new List<Node>();
    protected int currentChild = 0;

    public override void OnStart() { }

    public override void SetBehaviourTree(EnemyBT enemyBT)
    {
        base.SetBehaviourTree(enemyBT);
        foreach (Node child in children)
        {
            child.SetBehaviourTree(enemyBT);
        }
    }

    public override Node Instantiate()
    {
        CompositeNode node = Instantiate(this);
        node.children = new List<Node>();

        foreach (Node child in children)
        {
            node.children.Add(child.Instantiate());
        }

        return node;
    }

    public override void OnStop()
    {
        currentChild = 0;
    }
}