using System;
using UnityEngine;

public class Target : MonoBehaviour
{
    public event Action<Target> OnDestroyed;

    private void OnDisable()
    {
        OnDestroyed?.Invoke(this);
    }
}
