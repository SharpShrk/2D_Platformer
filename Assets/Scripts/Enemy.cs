using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(EnemyHealthBar))]

public class Enemy : MonoBehaviour
{
    private float _hitPoints;
    private float _damage;
    private Rigidbody2D _rigidbody2D;
    private EnemyHealthBar _healthBar;


    private void Awake()
    {
        _hitPoints = 100f;
        _damage = 10f;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _healthBar = GetComponent<EnemyHealthBar>();
    }

    
    private void Update()
    {
        
    }

    public void GetDamage(float damage)
    {
        _hitPoints -= damage; //добавить проверку на отрицательный урон

        if (_hitPoints <= 0)
        {
            Debug.Log("Враг умер");
            //запустить анимацию смерти, не забыть у игрока сделать также
        }

        _healthBar.SetHitPoints(damage);
    }

    //через invoke(nameof(name), 2f) сделать выстрелы
}
