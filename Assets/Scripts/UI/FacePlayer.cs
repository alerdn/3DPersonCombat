using UnityEngine;

public class FacePlayer : MonoBehaviour
{
    private Transform _player;

    private void Start()
    {
        _player = PlayerStateMachine.Instance.MainCameraTransform;
    }

    private void Update()
    {
        transform.forward = _player.forward;
    }
}