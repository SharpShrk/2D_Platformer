using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(EnemyHealthBar))]
[RequireComponent(typeof(Animator))]

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject _blast;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private LayerMask _playerLayerMask;
    [SerializeField] private Transform _attackZone;
    [SerializeField] private Player _player;

    private float _hitPoints;
    private float _attackZoneRange;
    private float _attackCooldown;
    private float _attackTimer;
    private float _jumpTimer;
    private float _jumpCooldown;
    private float _jumpHeight;
    private bool _isPlayerInAttackZone;
    private bool _isDrawingModGizmos;
    private bool _isDead;
    private bool _playerIsDead;
    private Rigidbody2D _rigidbody2D;
    private EnemyHealthBar _healthBar;
    private Animator _animator;

    private void Awake()
    {
        _hitPoints = 100f;
        _isDead = false;
        _isPlayerInAttackZone = false;
        _attackCooldown = 4f;
        _attackTimer = 1f;
        _jumpTimer = 6f;
        _jumpCooldown = 3f;
        _jumpHeight = 400f;
        _attackZoneRange = 8f;
        _isDrawingModGizmos = true;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _healthBar = GetComponent<EnemyHealthBar>();
    }

    
    private void Update()
    {
        if (_isDead == false)
        {
            Attack();
            Jump();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;

        if (_isDrawingModGizmos)
        {
            Gizmos.DrawWireSphere(_attackZone.position, _attackZoneRange);
        }
    }

    public void GetDamage(float damage)
    {
        _hitPoints -= damage;

        if (_hitPoints <= 0)
        {
            _isDead = true;
            _animator.SetTrigger("Dead");
            Destroy(gameObject, 1.5f);
        }

        _healthBar.SetHitPoints(damage);
        _animator.SetTrigger("Damaged");
    }

    private bool GetPlayerInAttackZone()
    {
        Collider2D[] players = Physics2D.OverlapCircleAll(_attackZone.position, _attackZoneRange, _playerLayerMask);

        if (players.Length != 0)
        {
            for (int i = 0; i < players.Length; i++)
            {
                return true;
            }
        }

        return false;
    }

    private void Attack()
    {
        _isPlayerInAttackZone = GetPlayerInAttackZone();
        

        if (_attackTimer <= 0f && _isPlayerInAttackZone == true && _player.IsDead == false)
        {
            _animator.SetTrigger("Attack");
            Instantiate(_blast, _firePoint.transform);
            _attackTimer = _attackCooldown;
        }
        else if (_player.IsDead == true)
        {
            _animator.SetBool("PlayerDead", true);
        }
        else
        {
            _attackTimer -= Time.deltaTime;
        }
    }

    private void Jump()
    {
        _isPlayerInAttackZone = GetPlayerInAttackZone();

        if (_jumpTimer <= 0f && _isPlayerInAttackZone == true && _player.IsDead == false)
        {
            _animator.SetTrigger("Jump");
            _rigidbody2D.AddForce(new Vector2(0 , _jumpHeight));
            _jumpTimer = _jumpCooldown;
        }
        else
        {
            _jumpTimer -= Time.deltaTime;
        }
    }

}
