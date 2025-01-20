using UnityEngine;

public abstract class BehaviourTree : MonoBehaviour
{
    [field: SerializeField] public Node Root { get; protected set; }

    private void Update()
    {
        Root?.Evaluate(Time.deltaTime);
    }

    protected abstract Node SetupTree();
}