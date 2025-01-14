using UnityEngine;

public abstract class BehaviourTree : MonoBehaviour
{
    public Node Root { get; private set; }

    protected virtual void Start()
    {
        Root = SetupTree();
    }

    private void Update()
    {
        Root?.Evaluate(Time.deltaTime);
    }

    protected abstract Node SetupTree();
}