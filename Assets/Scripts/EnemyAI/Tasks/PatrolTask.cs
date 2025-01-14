using UnityEngine;

public class PatrolTask : EnemyNodeBase
{
    private readonly int LocomotionHash = Animator.StringToHash("Locomotion");
    private readonly int SpeedHash = Animator.StringToHash("Speed");
    private Transform[] _waypoints;

    private int _currentWaypointIndex;

    private float _waitTime = .1f;
    private float _waitCounter = 0f;
    private bool _isWaiting = false;

    public PatrolTask(EnemyBT tree, Transform[] waypoints) : base(tree)
    {
        _waypoints = waypoints;
    }

    public PatrolTask(EnemyBT tree, string name, int priority, Transform[] waypoints) : base(tree, name, priority)
    {
        _waypoints = waypoints;
    }

    public override void OnStart()
    {
        tree.Animator.CrossFadeInFixedTime(LocomotionHash, .1f);
    }

    public override NodeState OnUpdate(float deltaTime)
    {
        if (_isWaiting)
        {
            _waitCounter += Time.deltaTime;

            if (_waitCounter >= _waitTime)
            {
                _isWaiting = false;
                tree.Animator.SetFloat(SpeedHash, 1f, .1f, Time.deltaTime);
            }
        }
        else
        {
            Transform wp = _waypoints[_currentWaypointIndex];
            if (Vector3.Distance(tree.transform.position, wp.position) <= tree.Agent.stoppingDistance)
            {
                _waitCounter = 0f;
                _isWaiting = true;

                _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;
                tree.Animator.SetFloat(SpeedHash, 0f, .1f, Time.deltaTime);
            }
            else
            {
                if (tree.Agent.isOnNavMesh)
                {
                    tree.Agent.destination = wp.position;
                    Move(tree.Agent.desiredVelocity.normalized * tree.MovementSpeed, deltaTime);
                }
                tree.Agent.velocity = tree.CharacterController.velocity;

                tree.transform.LookAt(wp.position);
                tree.Animator.SetFloat(SpeedHash, 1f, .1f, Time.deltaTime);
            }
        }

        return NodeState.Running;
    }

    public override void OnStop()
    {
    }

    public override void Reset()
    {
        _waitCounter = 0f;
        _isWaiting = false;
    }
}