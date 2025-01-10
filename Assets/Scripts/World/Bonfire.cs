using UnityEngine;

public class Bonfire : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;

    public void Rest()
    {
        PlayerStateMachine player = PlayerStateMachine.Instance;
        player.SwitchState(new PlayerRestingState(player));

        // player.SetPosition(_spawnPoint.position);
        player.RestoreStats();
        player.Inventory.ReplanishPotions();

        CheckpointManager.Instance.SetLastCheckpoint(_spawnPoint.position);

        EnemyManager.Instance.RespawnAll();

        AudioManager.Instance.PlayCue("Rest");
    }
}