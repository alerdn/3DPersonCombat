using System.Collections.Generic;
using UnityEngine;

public class Targeter : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private List<Target> _targets = new List<Target>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Target target))
        {
            _targets.Add(target);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Target target) && _targets.Contains(target))
        {
            _targets.Remove(target);
        }
    }

}