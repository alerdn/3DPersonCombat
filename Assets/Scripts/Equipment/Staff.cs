using UnityEngine;

public class Staff : Weapon
{
    private Spell _currentSpell;

    public void SetAttack(Spell spell, Health targetHealth, int intelligence, Vector3 castPoint)
    {
        _currentSpell = spell;

        _currentSpell.SetAttack(targetHealth, castPoint, GetDamageBase(intelligence));
    }

    public void Cast()
    {
        _currentSpell.Cast();
    }
}