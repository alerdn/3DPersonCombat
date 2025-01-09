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
        if (_stateMachine.CurrentWeapon is MeleeWeapon weapon)
        {
            weapon.EnableHitBox();
        }
    }

    public void DisableHitBox()
    {
        if (_stateMachine.CurrentWeapon is MeleeWeapon weapon)
        {
            weapon.DisableHitBox();
        }
    }

    public void CastSpell()
    {
        if (_stateMachine.CurrentWeapon is Staff staff)
        {
            staff.Cast();
        }
    }
}