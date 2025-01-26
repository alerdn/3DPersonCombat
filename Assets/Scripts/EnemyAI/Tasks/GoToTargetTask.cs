using UnityEngine;

[CreateAssetMenu(menuName = "BehaviourTree/Tasks/GoToTargetTask")]
public class GoToTargetTask : Node
{
    private readonly int LocomotionHash = Animator.StringToHash("Locomotion");
    private readonly int SpeedHash = Animator.StringToHash("Speed");

    public override void OnStart()
    {
        tree.Animator.CrossFadeInFixedTime(LocomotionHash, .1f);
    }

    public override NodeState OnUpdate(float deltaTime)
    {
        FacePlayer();
        MoveTo(tree.Player.transform.position, tree.MovementSpeed, deltaTime);

        tree.Animator.SetFloat(SpeedHash, tree.Agent.velocity.magnitude, .1f, deltaTime);
        return NodeState.Running;
    }

    public override void OnStop()
    {
        if (tree.Agent.isOnNavMesh)
        {
            tree.Agent.ResetPath();
        }
        tree.Agent.velocity = Vector3.zero;
    }
}