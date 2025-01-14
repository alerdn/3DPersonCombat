using UnityEngine;

public abstract class EnemyNodeBase : Node
{
    protected EnemyBT tree;

    public EnemyNodeBase(EnemyBT tree)
    {
        this.tree = tree;
    }

    public EnemyNodeBase(EnemyBT tree, string name, int priority) : base(name, priority)
    {
        this.tree = tree;
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
}