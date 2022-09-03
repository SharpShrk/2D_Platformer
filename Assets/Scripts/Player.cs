using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]

public class Player : MonoBehaviour
{
    [SerializeField] private Collider2D _attackRange;
    
    private Animator _animator;
    private Rigidbody2D _rigidbody2D;
    private bool _isAttack;
    private float _hitPoints;
    private float _damage;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _isAttack = false;
        _hitPoints = 100f;
        _damage = 10f;
    }

    private void Update()
    {
        _isAttack = _animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Attack");

        if (Input.GetMouseButtonDown(0) && _isAttack == false)
        {
            _animator.SetTrigger("Attack");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Enemy>(out Enemy enemy) && _isAttack == true)
        {
            enemy.GetDamage(_damage);
            //если объект не выходит из триггера, то второй удар не наносится. Сделать StayTrigger и добавить проверку, чтобы не наносилось много урона
        }
    }
}
