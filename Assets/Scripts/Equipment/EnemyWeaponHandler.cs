using UnityEngine;

public class EnemyWeaponHandler : MonoBehaviour
{
    private EnemyStateMachine _stateMachine;
    private EnemyBT _tree;

    private void Awake()
    {
        _stateMachine = GetComponent<EnemyStateMachine>();
        _tree = GetComponent<EnemyBT>();
    }

    public void EnableHitBox()
    {
        _stateMachine?.Weapon.EnableHitBox();
        _tree?.Weapon.EnableHitBox();
    }

    public void DisableHitBox()
    {
        _stateMachine?.Weapon.DisableHitBox();
        _tree?.Weapon.DisableHitBox();
    }
}