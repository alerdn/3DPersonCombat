using UnityEngine;

[CreateAssetMenu(fileName = "Homing Soulmass Spell", menuName = "Spells/Homing Soulmass Spell")]
public class HomingSoulmassSpell : Spell
{
    private SpellEffect _lastEffect;

    public override void Cast()
    {
        if (_lastEffect != null)
        {
            Destroy(_lastEffect.gameObject);
        }

        _lastEffect = Instantiate(EffectPrefab, castPosition, Quaternion.identity);
        _lastEffect.StartCoroutine(FollowPlayer(_lastEffect.transform, 60f));
    }
}