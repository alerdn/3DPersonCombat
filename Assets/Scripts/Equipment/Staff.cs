using UnityEngine;

public class Staff : Weapon
{
    [SerializeField] private Transform _castPosition;

    private Spell _currentSpell;

    public void SetAttack(Spell spell, Health targetHealth, int intelligence)
    {
        _currentSpell = spell;

        _currentSpell.SetAttack(targetHealth, _castPosition.position, GetDamageBase(intelligence));
    }

    public void Cast()
    {
        _currentSpell.Cast();
    }
}