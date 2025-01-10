using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Soul Arrow Spell", menuName = "Spells/Soul Arrow Spell")]
public class SoulArrowSpell : Spell
{
    public override void Cast()
    {
        SpellEffect effect = Instantiate(EffectPrefab, castPosition, Quaternion.identity);
        effect.transform.rotation = Quaternion.LookRotation(PlayerStateMachine.Instance.transform.forward);
        effect.StartCoroutine(MoveEffectTo(effect.transform, targetHealth));
    }
}