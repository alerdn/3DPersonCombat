using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    [SerializeField] private List<EnemyStateMachine> _enemies;

    public void RespawnAll()
    {
        foreach (EnemyStateMachine enemy in _enemies)
        {
            enemy.Respawn();
        }
    }
}