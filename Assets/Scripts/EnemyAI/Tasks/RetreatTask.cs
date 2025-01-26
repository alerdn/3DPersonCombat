using UnityEngine;

[CreateAssetMenu(menuName = "BehaviourTree/Tasks/RetreatTask")]
public class RetreatTask : Node
{
    private readonly int LocomotionHash = Animator.StringToHash("Locomotion");
    private readonly int SpeedHash = Animator.StringToHash("Speed");

    [SerializeField] private float _retreatDuration = 2f;

    private float _retreatCount;

    public override void OnStart()
    {
        tree.Animator.CrossFadeInFixedTime(LocomotionHash, .1f);
        tree.Animator.SetFloat(SpeedHash, 0f);
    }

    public override NodeState OnUpdate(float deltaTime)
    {
        tree.Animator.SetFloat(SpeedHash, -1f, .1f, deltaTime);

        _retreatCount += deltaTime;

        if (_retreatCount >= _retreatDuration)
        {
            return NodeState.Success;
        }

        Vector3 direction = tree.transform.forward * -1f;
        Vector3 position = tree.transform.position + direction;

        MoveTo(position, tree.RetreatSpeed, deltaTime);
        FacePlayer();

        return NodeState.Running;
    }

    public override void OnStop()
    {
        _retreatCount = 0f;
    }
}