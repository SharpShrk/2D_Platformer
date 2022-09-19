using System;
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

    private float _healthPoints = 100f;
    private float _attackZoneRange = 8f;
    private float _attackCooldown = 4f;
    private float _attackTimer = 1f;
    private float _jumpTimer = 1f;
    private float _jumpCooldown = 3f;
    private float _jumpHeight = 400f;
    private bool _isPlayerInAttackZone;
    private bool _isDrawingModGizmos;
    private bool _isDead;
    private bool _playerIsDead;
    private Rigidbody2D _rigidbody2D;
    private EnemyHealthBar _healthBar;
    private Animator _animator;

    public event Action EventEnemyTakeDamage;

    public float HealthPoints => _healthPoints;

    private void Awake()
    {
        _isDead = false;
        _isPlayerInAttackZone = false;
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

    public void TakeDamage(float damage)
    {
        _healthPoints -= damage;

        if (_healthPoints <= 0)
        {
            _isDead = true;
            _animator.SetTrigger(AnimatorEnemyController.Params.Dead);
            Destroy(gameObject, 1.5f);
        }

        EventEnemyTakeDamage?.Invoke();
        //_healthBar.SetHitPoints(damage);
        _animator.SetTrigger(AnimatorEnemyController.Params.Damaged);
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
            _animator.SetTrigger(AnimatorEnemyController.Params.Attack);
            Instantiate(_blast, _firePoint.transform);
            _attackTimer = _attackCooldown;
        }
        else if (_player.IsDead == true)
        {
            _animator.SetBool(AnimatorEnemyController.Params.PlayerDead, true);
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
            _animator.SetTrigger(AnimatorEnemyController.Params.Jump);
            _rigidbody2D.AddForce(new Vector2(0 , _jumpHeight));
            _jumpTimer = _jumpCooldown;
        }
        else
        {
            _jumpTimer -= Time.deltaTime;
        }
    }
}

public static class AnimatorEnemyController
{
    public static class Params
    {
        public const string Dead = nameof(Dead);
        public const string Damaged = nameof(Damaged);
        public const string Attack = nameof(Attack);
        public const string Jump = nameof(Jump);
        public const string PlayerDead = nameof(PlayerDead);
    }
}
