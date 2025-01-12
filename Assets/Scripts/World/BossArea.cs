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
        _boss.Health.OnDie -= OnDie;
    }

    private void Update()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, transform.localScale / 2, Quaternion.identity, LayerMask.GetMask("Player"));
        if (colliders.Length > 0 && !_boss.Health.IsDead)
        {
            ShowBossHealth();
        }
        else
        {
            HideBossHealth();
        }
    }

    private void OnDie()
    {
        HideBossHealth();
    }

    private void UpdateBossHealthBar(int arg1, int arg2)
    {
        _bossHealthBar.fillAmount = (float)arg1 / (float)arg2;
    }

    public void ShowBossHealth()
    {
        _canvas.SetActive(true);
    }

    private void HideBossHealth()
    {
        _canvas.SetActive(false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}
