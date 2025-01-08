using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [SerializeField] private UnityEvent _onInteract;
    [SerializeField] private UnityEvent _inRangeEvent;
    [SerializeField] private UnityEvent _outOfRangeEvent;

    [SerializeField] private GameObject _canvas;

    [SerializeField] private bool _inRange;

    private void Start()
    {
        _inRange = false;
        _canvas.SetActive(false);

        PlayerStateMachine.Instance.InputReader.InteractEvent += OnInteract;
    }

    private void OnDestroy()
    {
        PlayerStateMachine.Instance.InputReader.InteractEvent -= OnInteract;
    }

    private void OnInteract()
    {
        if (!_inRange) return;

        _inRange = false;
        _onInteract?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !_inRange)
        {
            _inRange = true;
            _inRangeEvent?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _inRange = false;
            _outOfRangeEvent?.Invoke();
        }
    }
}