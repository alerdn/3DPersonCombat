using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessageUI : MonoBehaviour
{
    [SerializeField] private Health _bossHealth;
    [SerializeField] private GameObject _deathText;
    [SerializeField] private GameObject _recoverText;
    [SerializeField] private GameObject _winText;

    private void Start()
    {
        _deathText.gameObject.SetActive(false);
        _recoverText.gameObject.SetActive(false);
        _winText.gameObject.SetActive(false);

        PlayerStateMachine.Instance.Health.OnDie += ShowDeathMessage;
        PlayerStateMachine.Instance.Inventory.OnSoulsRecovered += ShowRecoverMessage;
        _bossHealth.OnDie += ShowWinMessage;
    }

    private void OnDestroy()
    {
        PlayerStateMachine.Instance.Health.OnDie -= ShowDeathMessage;
        PlayerStateMachine.Instance.Inventory.OnSoulsRecovered -= ShowRecoverMessage;
        _bossHealth.OnDie -= ShowWinMessage;
    }

    public void ShowDeathMessage()
    {
        StartCoroutine(ShowMessage(_deathText, 4f));
    }

    public void ShowRecoverMessage()
    {
        StartCoroutine(ShowMessage(_recoverText, 2f));
    }

    public void ShowWinMessage()
    {
        AudioManager.Instance.PlayCue("Victory");
        StartCoroutine(ShowMessage(_winText, 6f));
    }

    private IEnumerator ShowMessage(GameObject gameObject, float duration)
    {
        gameObject.SetActive(true);
        yield return new WaitForSeconds(duration);
        gameObject.SetActive(false);
    }
}
