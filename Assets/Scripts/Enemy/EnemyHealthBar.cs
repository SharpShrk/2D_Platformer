using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Image _bar;

    private float _fill;
    private float _damageRatio = 0.01f;

    void Start()
    {
        _fill = 1f;
    }

    void Update()
    {
        if (_fill > 1f)
        {
            _fill = 1f;
        }

        if (_fill < 0f)
        {
            _fill = 0f;
        }

        _bar.fillAmount = _fill;
    }

    public void SetHitPoints(float damage)
    {
        _fill -= damage * _damageRatio;
    }
}
