using UnityEngine;

[CreateAssetMenu(menuName = "BehaviourTree/Tasks/IdleTask")]
public class IdleTask : Node
{
    private readonly int LocomotionHash = Animator.StringToHash("Locomotion");
    private readonly int SpeedHash = Animator.StringToHash("Speed");

    public override void OnStart()
    {
        tree.Animator.CrossFadeInFixedTime(LocomotionHash, .1f);
    }

    public override NodeState OnUpdate(float deltaTime)
    {
        tree.Animator.SetFloat(SpeedHash, 0f);

        return NodeState.Success;
    }

    public override void OnStop() { }
}