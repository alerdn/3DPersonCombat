using UnityEngine;

[CreateAssetMenu(menuName = "BehaviourTree/Tasks/AttackTask")]
public class AttackTask : Node
{
    [field: SerializeField] public int AttackIndex { get; set; }

    private MeleeWeapon _weapon;
    private Attack _attack;

    public override void OnStart()
    {
        _weapon = tree.Weapon;
        _attack = _weapon.Attacks[AttackIndex];

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