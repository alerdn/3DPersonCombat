using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportDoor : MonoBehaviour
{
    [SerializeField] private Transform _destination;

    public void Teleport()
    {
        PlayerStateMachine.Instance.SetPosition(_destination.position);
    }
}
