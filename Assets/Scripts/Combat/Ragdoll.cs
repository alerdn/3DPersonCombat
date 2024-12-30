using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private CharacterController _controller;

    private Collider[] _colliders;
    private Rigidbody[] _rigidbodies;

    private void Awake()
    {
        _colliders = GetComponentsInChildren<Collider>(true);
        _rigidbodies = GetComponentsInChildren<Rigidbody>();
    }

    private void Start()
    {
        ToggleRagdoll(false);
    }

    public void ToggleRagdoll(bool isRagdoll)
    {
        foreach (Collider collider in _colliders)
        {
            if (collider.CompareTag("Ragdoll"))
            {
                collider.enabled = isRagdoll;
            }
        }

        foreach (Rigidbody rigidbody in _rigidbodies)
        {
            if (rigidbody.CompareTag("Ragdoll"))
            {
                rigidbody.isKinematic = !isRagdoll;
                rigidbody.useGravity = isRagdoll;
            }
        }

        _animator.enabled = !isRagdoll;
        _controller.enabled = !isRagdoll;
    }
}
