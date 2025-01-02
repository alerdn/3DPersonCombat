using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : Singleton<CheckpointManager>
{
    [SerializeField] private List<Transform> _checkpoints;

    private Vector3 _lastCheckpoint;

    private void Start()
    {
        SetLastCheckpoint(PlayerStateMachine.Instance.transform.position);
    }

    public Vector3 GetLastCheckpoint()
    {
        return _lastCheckpoint;
    }

    public void SetLastCheckpoint(Vector3 checkpoint)
    {
        _lastCheckpoint = checkpoint;
    }
}