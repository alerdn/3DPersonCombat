using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossArea : MonoBehaviour
{
    [SerializeField] EnemyStateMachine _boss;
    [SerializeField] private GameObject _canvas;
    [SerializeField] private Image _bossHealthBar;

    private void Start()
    {
        _boss.Health.OnHealthChanged += UpdateBossHealthBar;

        _boss.Health.OnDie += OnDie;

        _canvas.SetActive(false);
    }

    private void OnDestroy()
    {
        _boss.Health.OnHealthChanged -= UpdateBossHealthBar;
    }

    private void OnDie()
    {
        HideBossHealth();
        _boss.Health.OnDie -= HideBossHealth;
    }

    private void UpdateBossHealthBar(int arg1, int arg2)
    {
        _bossHealthBar.fillAmount = (float)arg1 / (float)arg2;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ShowBossHealth();
            ShowBossHealth();
            PlayerStateMachine.Instance.Health.OnDie += HideBossHealth;
        }
    }

    public void ShowBossHealth()
    {
        _canvas.SetActive(true);
    }

    private void HideBossHealth()
    {
        _canvas.SetActive(false);
        PlayerStateMachine.Instance.Health.OnDie -= HideBossHealth;
    }
}
