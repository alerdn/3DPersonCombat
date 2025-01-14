using UnityEngine;

public class AttackTask : EnemyNodeBase
{
    private MeleeWeapon _weapon;
    private Attack _attack;

    public AttackTask(EnemyBT tree, int attackIndex) : base(tree)
    {
        _weapon = tree.Weapon;
        _attack = _weapon.Attacks[attackIndex];
    }

    public override void OnStart()
    {
        int damage = _weapon.GetAttackDamage(_attack);
        _weapon.SetAttack(damage, _attack.Knockback, UnitType.Player);

        FacePlayer();
        tree.Animator.CrossFadeInFixedTime(_attack.AnimationName, .1f);
        AudioManager.Instance.PlayCue("Swing");
    }

    public override NodeState OnUpdate(float deltaTime)
    {
        float normalizedTime = GetNormalizedTime(tree.Animator, "Attack");

        if (normalizedTime < .3f)
        {
            FacePlayer();
        }

        if (normalizedTime >= 1f)
        {
            return NodeState.Success;
        }

        return NodeState.Running;
    }

    public override void OnStop()
    {
    }
}