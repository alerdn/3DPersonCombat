using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Arrow Spell", menuName = "Spells/Arrow Spell")]
public class SoulArrowSpell : Spell
{
    public override void Cast()
    {
        SpellEffect effect = Instantiate(EffectPrefab, castPosition, Quaternion.identity);
        effect.transform.rotation = Quaternion.LookRotation(PlayerStateMachine.Instance.transform.forward);
        effect.StartCoroutine(MoveEffectTo(effect.transform, targetHealth));

        Debug.Log($"Casting {Name} at {targetHealth?.name ?? "random"} for {GetDamage()} damage");
    }
}