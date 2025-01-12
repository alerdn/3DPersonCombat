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

    private void Update()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, transform.localScale / 2, Quaternion.identity, LayerMask.GetMask("Player"));

        if (colliders.Length > 0 && !_inRange)
        {
            _inRange = true;
            _inRangeEvent?.Invoke();
        }
        else if (colliders.Length == 0 && _inRange)
        {
            _inRange = false;
            _outOfRangeEvent?.Invoke();
        }
    }

    private void OnInteract()
    {
        if (!_inRange) return;

        _onInteract?.Invoke();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}