using UnityEngine;

[CreateAssetMenu(menuName = "BehaviourTree/WaitNode")]
public class WaitNode : Node
{
    private readonly float _duration;
    private float _waitCount;

    public override void OnStart() { }

    public override NodeState OnUpdate(float deltaTime)
    {
        _waitCount += Time.deltaTime;

        if (_waitCount >= _duration)
        {
            return NodeState.Success;
        }

        return NodeState.Running;
    }

    public override void OnStop() { }

    public override void Reset()
    {
        _waitCount = 0f;
    }
}