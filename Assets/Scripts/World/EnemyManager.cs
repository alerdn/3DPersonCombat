using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    [SerializeField] private List<EnemyStateMachine> _enemies;

    private void Start()
    {
        _enemies = new List<EnemyStateMachine>(GetComponentsInChildren<EnemyStateMachine>());
    }

    public void RespawnAll()
    {
        foreach (EnemyStateMachine enemy in _enemies)
        {
            enemy.Respawn();
        }
    }
}