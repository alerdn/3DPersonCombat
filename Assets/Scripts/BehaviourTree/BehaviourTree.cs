using UnityEngine;

public abstract class BehaviourTree : MonoBehaviour
{
    public Node CurrentNode;
    [field: SerializeField] public Node Root { get; protected set; }

    private void Update()
    {
        Root?.Evaluate(Time.deltaTime, out CurrentNode);
    }

    protected abstract Node SetupTree();
}