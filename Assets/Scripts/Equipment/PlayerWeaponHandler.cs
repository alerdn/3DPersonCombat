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

    public void Fire(int attackIndex)
    {
        Vector3 targetPosition = _stateMachine.Targeter.CurrentTarget?.transform.position ?? _stateMachine.transform.forward * 100f;
        int damage = _stateMachine.CurrentWeapon.Attacks[attackIndex].Damage;

        _stateMachine.CurrentWeapon?.Fire(targetPosition, damage);
    }
}