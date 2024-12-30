using UnityEngine;
using UnityEngine.AI;

public class ForceReceiver : MonoBehaviour
{
    public Vector3 Movement => _impact + Vector3.up * _verticalVelocity;

    [SerializeField] private CharacterController _controller;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private float _drag = .1f;

    private float _verticalVelocity;
    private Vector3 _impact;
    private Vector3 _dampingVelocity;

    private void Update()
    {
        if (_verticalVelocity < 0f && _controller.isGrounded)
        {
            _verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            _verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }

        _impact = Vector3.SmoothDamp(_impact, Vector3.zero, ref _dampingVelocity, _drag);

        if (_impact.sqrMagnitude < .2f * .2f)
        {
            if (_agent)
            {
                _impact = Vector3.zero;
                _agent.enabled = true;
            }
        }
    }

    public void AddForce(Vector3 force)
    {
        _impact += force;
        if (_agent)
        {
            _agent.enabled = false;
        }
    }

    public void Jump(float jumpForce)
    {
        _verticalVelocity += jumpForce;
    }
}