using System.Collections.Generic;
using UnityEngine;

public class Spellbook : MonoBehaviour
{
    public Spell CurrentSpell
    {
        get
        {
            if (_currentSpellIndex >= _spells.Count) return null;
            return _spells[_currentSpellIndex];
        }
    }

    [SerializeField] private List<Spell> _spells = new List<Spell>();

    private int _currentSpellIndex;

    public void SwitchSpell()
    {
        _currentSpellIndex = (_currentSpellIndex + 1) % _spells.Count;
    }
}