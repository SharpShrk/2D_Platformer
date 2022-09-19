using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Image _bar;
    [SerializeField] private Enemy _enemy;

    private float _damageRatio = 0.01f;

    private void OnEnable()
    {
        _enemy.EventEnemyTakeDamage += SetHitPoints;
    }

    private void OnDisable()
    {
        _enemy.EventEnemyTakeDamage -= SetHitPoints;
    }

    void Start()
    {
        _bar.fillAmount = _enemy.HealthPoints * _damageRatio;
    }

    public void SetHitPoints()
    {
        _bar.fillAmount = _enemy.HealthPoints * _damageRatio;
    }
}
