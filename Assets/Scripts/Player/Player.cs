using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerHealthBar))]
[RequireComponent(typeof(Inventory))]
[RequireComponent(typeof(AudioManager))]
[RequireComponent(typeof(PlayerMovement))]

public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask _enemyLayerMask;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private HealingPotion _healingPotion;
    [SerializeField] private ParticleSystem _particleHealing;

    private Animator _animator;
    private PlayerHealthBar _healthBar;
    private Inventory _inventory;
    private AudioManager _audioManager;
    private PlayerMovement _playerMovement;
    private float _healthPoints;
    private float _maxHealthPoints;
    private float _damage;
    private float _attackRange;
    private float _attackColdown;
    private float _attackTimer;
    private bool _isDrawingModGizmos;
    private bool _isDead;

    public bool IsDead => _isDead;

    public float HealthPoints => _healthPoints;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _healthBar = GetComponent<PlayerHealthBar>();
        _inventory = GetComponent<Inventory>();
        _audioManager = GetComponent<AudioManager>();
        _playerMovement = GetComponent<PlayerMovement>();
        _isDrawingModGizmos = true;
        _isDead = false;
        _healthPoints = 10f;
        _maxHealthPoints = 100f;
        _damage = 10f;
        _attackRange = 0.5f;
        _attackColdown = 0.5f;
        _attackTimer = 0f;
    }

    private void Update()
    {
        Attack();

        if (Input.GetKeyDown(KeyCode.F) && _inventory.NumberOfPotions > 0)
        {
            GetHeal();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        if (_isDrawingModGizmos)
        {
            Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
        }
    }

    private void Attack()
    {
        if (_attackTimer <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _animator.SetTrigger("Attack");
                _audioManager.PlayingSwordAttackSound();

                Collider2D[] enemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _enemyLayerMask);

                if (enemies.Length != 0)
                {
                    for (int i = 0; i < enemies.Length; i++)
                    {
                        enemies[i].GetComponent<Enemy>().GetDamage(_damage);
                    }
                }

                _attackTimer = _attackColdown;
            }
        }
        else
        {
            _attackTimer -= Time.deltaTime;
        }
    }

    private void GetHeal()
    {
        int potionUsed = -1;
        _healthPoints += _healingPotion.Healing();

        if (_healthPoints >= _maxHealthPoints)
        {
            _healthPoints = _maxHealthPoints;
        }

        _inventory.ChangeNumberOfPotions(potionUsed);
        _particleHealing.Play();
        _audioManager.PlayingPotionUsedClip();
    }

    public void GetDamage(float damage)
    {
        if (damage >= 0)
        {
            _healthPoints -= damage;
            _animator.SetTrigger("Hitting");

            if (_healthPoints <= 0)
            {
                _playerMovement.enabled = false;
                _healthPoints = 0;
                _isDead = true;
                _animator.SetTrigger("Dead");
            }
        }        
    }    
}
