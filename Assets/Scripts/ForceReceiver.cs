using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    public Vector3 Movement => _impact + Vector3.up * _verticalVelocity;

    [SerializeField] private CharacterController _controller;
    [SerializeField] private float _drag;

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
    }

    public void AddForce(Vector3 force)
    {
        _impact += force;
    }
}