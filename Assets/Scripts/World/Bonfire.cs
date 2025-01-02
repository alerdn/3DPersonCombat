using UnityEngine;

public class Bonfire : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;

    public void Rest()
    {
        PlayerStateMachine player = PlayerStateMachine.Instance;

        player.SetPosition(_spawnPoint.position);
        player.Health.RestoreHealth();
        player.Inventory.ReplanishHealItem();

        CheckpointManager.Instance.SetLastCheckpoint(_spawnPoint.position);

        EnemyManager.Instance.RespawnAll();
    }
}