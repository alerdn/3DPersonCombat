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
        Attack attack = _stateMachine.CurrentWeapon.Attacks[attackIndex];

        _stateMachine.CurrentWeapon?.Fire(_stateMachine.CharacterController, targetPosition, attack.Damage, attack.Knockback);
    }
}