using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Targeter : MonoBehaviour
{
    public Target CurrentTarget { get; private set; }

    [SerializeField] private CinemachineTargetGroup _cineTargetGroup;

    [Header("Debug")]
    [SerializeField] private List<Target> _targets = new List<Target>();
    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    public bool SelectTarget()
    {
        if (_targets.Count == 0) return false;

        Target closestTarget = null;
        float closestTargetDistance = Mathf.Infinity;

        foreach (Target target in _targets)
        {
            // WorldToViewportPoint retorna valores entre 0 e 1
            Vector2 viewPos = _mainCamera.WorldToViewportPoint(target.transform.position);

            if (!target.GetComponentInChildren<Renderer>().isVisible)
            {
                continue;
            }

            Vector2 toCenter = viewPos - new Vector2(.5f, .5f);
            // sqrMagnitude is faster than magnitude
            if (toCenter.sqrMagnitude < closestTargetDistance)
            {
                closestTarget = target;
                closestTargetDistance = toCenter.sqrMagnitude;
            }
        }

        if (closestTarget == null) return false;

        CurrentTarget = closestTarget;
        _cineTargetGroup.AddMember(CurrentTarget.transform, 1f, 2f);

        return true;
    }

    public void CancelTarget()
    {
        if (CurrentTarget == null) return;

        _cineTargetGroup.RemoveMember(CurrentTarget.transform);
        CurrentTarget = null;
    }

    public void ClearTargets()
    {
        foreach (Target target in _targets)
        {
            _cineTargetGroup.RemoveMember(target.transform);
        }

        _targets.Clear();
        CurrentTarget = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Target target))
        {
            if (target.isActiveAndEnabled)
            {
                AddTarget(target);
            }
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
        target.OnDisabled += RemoveTarget;
    }

    private void RemoveTarget(Target target)
    {
        if (CurrentTarget == target)
        {
            _cineTargetGroup.RemoveMember(CurrentTarget.transform);
            CurrentTarget = null;
        }

        target.OnDisabled -= RemoveTarget;
        _targets.Remove(target);
    }

}