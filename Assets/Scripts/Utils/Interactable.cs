using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [SerializeField] private UnityEvent _onInteract;

    private bool _inRange;

    private void Start()
    {
        _inRange = false;

        PlayerStateMachine.Instance.InputReader.InteractEvent += OnInteract;
    }

    private void OnDestroy()
    {
        PlayerStateMachine.Instance.InputReader.InteractEvent -= OnInteract;
    }

    private void OnInteract()
    {
        if (!_inRange) return;

        _onInteract?.Invoke();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _inRange = false;
        }
    }
}