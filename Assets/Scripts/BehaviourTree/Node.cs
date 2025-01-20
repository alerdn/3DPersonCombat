using System;
using System.Collections.Generic;
using UnityEngine;

public enum NodeState
{
    Running,
    Success,
    Failure
}

public abstract class Node : ScriptableObject
{
    public Node Parent;
    public readonly string Name;
    public Node CurrentNode;

    protected EnemyBT tree;

    private bool _isStarted;
    private Dictionary<string, object> _dataContext = new Dictionary<string, object>();

    public virtual void SetBehaviourTree(EnemyBT enemyBT)
    {
        tree = enemyBT;
    }

    public virtual Node Instantiate()
    {
        return Instantiate(this);
    }

    public abstract void OnStart();
    public abstract NodeState OnUpdate(float deltaTime);
    public abstract void OnStop();
    public virtual void OnReset() { }

    public NodeState Evaluate(float deltaTime, out Node currentNode)
    {
        currentNode = this;
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

    public void Reset()
    {
        _isStarted = false;
        OnReset();
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

    protected void FacePlayer()
    {
        if (tree.Player == null) return;

        Vector3 lookPos = tree.Player.transform.position - tree.transform.position;
        lookPos.y = 0;

        tree.transform.rotation = Quaternion.LookRotation(lookPos);
    }

    protected bool IsInChaseRange()
    {
        if (tree.Player.Health.IsDead) return false;

        if (tree.HasNoticedPlayer) return true;

        float playerDistanceSqr = (tree.Player.transform.position - tree.transform.position).sqrMagnitude;

        return playerDistanceSqr <= tree.PlayerChasingRange * tree.PlayerChasingRange;
    }

    protected void Move(float deltaTime)
    {
        Move(Vector3.zero, deltaTime);
    }

    protected void Move(Vector3 motion, float deltaTime)
    {
        Vector3 force = tree.ForceReceiver.Movement;
        tree.CharacterController.Move((motion + force) * deltaTime);
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