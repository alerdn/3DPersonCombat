using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Targeter : MonoBehaviour
{
    public Target CurrentTarget { get; private set; }

    [SerializeField] private CinemachineTargetGroup _cineTargetGroup;

    private List<Target> _targets = new List<Target>();

    public bool SelectTarget()
    {
        if (_targets.Count == 0) return false;

        CurrentTarget = _targets[0];
        _cineTargetGroup.AddMember(CurrentTarget.transform, 1f, 2f);

        return true;
    }

    public void CancelTarget()
    {
        if (CurrentTarget == null) return;

        _cineTargetGroup.RemoveMember(CurrentTarget.transform);
        CurrentTarget = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Target target))
        {
            AddTarget(target);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Target target) && _targets.Contains(target))
        {
            RemoveTarget(target);
        }
    }

    private void AddTarget(Target target)
    {
        _targets.Add(target);
        target.OnDestroyed += RemoveTarget;
    }

    private void RemoveTarget(Target target)
    {
        if (CurrentTarget == target)
        {
            _cineTargetGroup.RemoveMember(CurrentTarget.transform);
            CurrentTarget = null;
        }

        target.OnDestroyed -= RemoveTarget;
        _targets.Remove(target);
    }

}