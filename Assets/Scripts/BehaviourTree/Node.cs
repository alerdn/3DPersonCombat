using System.Collections.Generic;
using UnityEngine;

public enum NodeState
{
    Running,
    Success,
    Failure
}

public abstract class Node
{
    public Node Parent;
    public readonly string Name;
    public readonly int Priority;

    protected List<Node> children = new List<Node>();
    protected int currentChild = 0;

    private bool _isStarted;

    private Dictionary<string, object> _dataContext = new Dictionary<string, object>();

    public Node(string name = "Node", int priority = 0)
    {
        Name = name;
        Priority = priority;
    }

    public Node(string name, int priority, List<Node> children)
    {
        Name = name;
        Priority = priority;

        foreach (Node child in children)
        {
            AddChild(child);
        }
    }

    public void AddChild(Node node)
    {
        node.Parent = this;
        children.Add(node);
    }

    public virtual void Reset()
    {
        currentChild = 0;
        foreach (Node child in children)
        {
            child.Reset();
        }
    }

    public abstract void OnStart();
    public abstract NodeState OnUpdate(float deltaTime);
    public abstract void OnStop();

    public NodeState Evaluate(float deltaTime)
    {
        if (!_isStarted)
        {
            OnStart();
            _isStarted = true;
        }

        NodeState state = OnUpdate(deltaTime);
        if (state != NodeState.Running)
        {
            OnStop();
            _isStarted = false;
        }

        return state;
    }

    public void SetData(string key, object value)
    {
        _dataContext[key] = value;
    }

    public object GetData(string key)
    {
        if (_dataContext.TryGetValue(key, out object value))
        {
            return value;
        }

        Node node = Parent;
        while (node != null)
        {
            value = node.GetData(key);
            if (value != null)
            {
                return value;
            }
            node = node.Parent;
        }

        return null;
    }

    public bool ClearData(string key)
    {
        if (_dataContext.ContainsKey(key))
        {
            _dataContext.Remove(key);
            return true;
        }

        Node node = Parent;
        while (node != null)
        {
            bool cleared = node.ClearData(key);
            if (cleared)
            {
                return true;
            }
            node = node.Parent;
        }

        return false;
    }

    protected float GetNormalizedTime(Animator animator, string tag)
    {
        AnimatorStateInfo currentInfop = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(0);

        if (animator.IsInTransition(0) && nextInfo.IsTag(tag))
        {
            return nextInfo.normalizedTime;
        }
        else if (!animator.IsInTransition(0) && currentInfop.IsTag(tag))
        {
            return currentInfop.normalizedTime;
        }
        else
        {
            return 0f;
        }
    }
}