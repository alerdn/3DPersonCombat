using UnityEngine;

public class EnemyWeaponHandler : MonoBehaviour
{
    private EnemyStateMachine _stateMachine;

    private void Awake()
    {
        _stateMachine = GetComponent<EnemyStateMachine>();
    }

    public void EnableHitBox()
    {
        _stateMachine.Weapon.EnableHitBox();
    }

    public void DisableHitBox()
    {
        _stateMachine.Weapon.DisableHitBox();
    }
}