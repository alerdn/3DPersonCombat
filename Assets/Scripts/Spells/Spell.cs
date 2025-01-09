using System.Collections;
using UnityEngine;

public abstract class Spell : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; protected set; }
    [field: SerializeField] public int ManaCost { get; private set; }
    [field: SerializeField] public int RawDamage { get; private set; }
    [field: SerializeField] public string AnimationName { get; private set; }
    [field: SerializeField] public SpellEffect EffectPrefab { get; private set; }

    protected Health targetHealth;
    protected Vector3 castPosition;
    private int _damageBase;

    public void SetAttack(Health targetHealth, Vector3 castPosition, int damageBase)
    {
        this.targetHealth = targetHealth;
        this.castPosition = castPosition;
        _damageBase = damageBase;
    }

    public abstract void Cast();

    protected int GetDamage()
    {
        return _damageBase + RawDamage;
    }

    protected IEnumerator MoveEffectTo(Transform effect, Health targetHealth)
    {
        // Cast sem target
        float spawnTime = Time.time;

        if (!targetHealth)
        {
            while (Time.time < spawnTime + 2f)
            {
                effect.position = Vector3.MoveTowards(effect.position, effect.position + effect.forward, 10 * Time.deltaTime);
                Collider[] colliders = Physics.OverlapSphere(effect.position, 0.25f, LayerMask.GetMask("Enemy"));

                if (colliders.Length > 0)
                {
                    targetHealth = colliders[0].GetComponent<Health>();
                    targetHealth.TakeDamage(GetDamage());
                    break;
                }

                yield return null;
            }
        }

        // Cast com target
        else
        {
            // A posição dos inimigos sempre é no pé, então precisa ajustar
            Vector3 targetPosition = targetHealth.transform.position + Vector3.up;
            float distanceToTarget = Vector3.Distance(effect.position, targetPosition);
            while (distanceToTarget > 0.1f)
            {
                targetPosition = targetHealth.transform.position + Vector3.up;
                distanceToTarget = Vector3.Distance(effect.position, targetPosition);
                effect.position = Vector3.MoveTowards(effect.position, targetPosition, 10 * Time.deltaTime);
                effect.LookAt(targetPosition);

                yield return null;
            }

            targetHealth.TakeDamage(GetDamage());
        }

        Destroy(effect.gameObject);
    }
}