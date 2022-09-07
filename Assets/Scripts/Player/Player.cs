using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]

public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask EnemyLayerMask;

    private Animator _animator;
    private Rigidbody2D _rigidbody2D;
    private float _hitPoints;
    private float _damage;
    private float _attackRange;
    private bool _isAttack;
    private bool _isDrawingModGizmos;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _isDrawingModGizmos = true;
        _isAttack = false;
        _hitPoints = 100f;
        _damage = 10f;
        _attackRange = 1.3f;
    }

    private void Update()
    {
        _isAttack = _animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Attack");

        if (Input.GetMouseButtonDown(0) && _isAttack == false)
        {
            _animator.SetTrigger("Attack");
        }

        AttackCollision();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        if (_isDrawingModGizmos)
        {
            Gizmos.DrawWireSphere(transform.position, _attackRange);
        }
    }

    private void AttackCollision()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, _attackRange, EnemyLayerMask);

        if (enemies.Length > 0)
        {
            Debug.Log(true);            
        }
        else
        {
            Debug.Log(false);
        }
    }


    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_enemyDetector.IsEnemiesAttacked && _isAttack == true)
        {
            enemy.GetDamage(_damage);
            //если объект не выходит из триггера, то второй удар не наносится. Сделать StayTrigger и добавить проверку, чтобы не наносилось много урона
        }
    }*/
}
