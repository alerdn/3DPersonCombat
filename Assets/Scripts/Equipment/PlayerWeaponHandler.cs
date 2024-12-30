using UnityEngine;

public class PlayerWeaponHandler : MonoBehaviour
{
    private PlayerStateMachine _stateMachine;

    private void Awake()
    {
        _stateMachine = GetComponent<PlayerStateMachine>();
    }

    public void EnableHitBox()
    {
        _stateMachine.CurrentWeapon?.EnableHitBox();
    }

    public void DisableHitBox()
    {
        _stateMachine.CurrentWeapon?.DisableHitBox();
    }
}