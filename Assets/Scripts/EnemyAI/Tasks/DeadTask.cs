using UnityEngine;

[CreateAssetMenu(menuName = "BehaviourTree/Tasks/DeadTask")]
public class DeadTask : Node
{

    public override void OnStart()
    {
        tree.Ragdoll.ToggleRagdoll(true);
        tree.Weapon.gameObject.SetActive(false);
        tree.Target.enabled = false;

        tree.HealthUI.gameObject.SetActive(false);

        PlayerStateMachine.Instance.Inventory.Souls += tree.SoulsValue;
    }

    public override NodeState OnUpdate(float deltaTime)
    {
        return NodeState.Success;
    }

    public override void OnStop() { }
}