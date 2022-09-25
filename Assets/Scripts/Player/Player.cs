using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Inventory))]
[RequireComponent(typeof(PlayerMovement))]

public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask _enemyLayerMask;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private HealingPotion _healingPotion;
    [SerializeField] private ParticleSystem _particleHealing;
    //[SerializeField] private float _healthPoints;

    private Animator _animator;
    private Inventory _inventory;
    private PlayerMovement _playerMovement;
    private Health _health;
    //private float _maxHealthPoints = 100f;
    private float _damage = 10f;
    private float _attackRange = 0.5f;
    private float _attackColdown = 0.5f;
    private float _attackTimer = 0f;
    private bool _isDrawingModGizmos;
    private bool _isDead = false;

    //public event Action EventHealthHasChanged;

    public bool IsDead => _isDead;
    //public float HealthPoints => _healthPoints;
    public Inventory GetInventory => _inventory;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _inventory = GetComponent<Inventory>();
        _playerMovement = GetComponent<PlayerMovement>();
        _health = GetComponent<Health>();
        _isDrawingModGizmos = true;
        //_healthPoints = 50f;
    }

    private void Update()
    {
        Attack();

        if (Input.GetKeyDown(KeyCode.F) && _inventory.NumberOfPotions > 0)
        {
            Heal();
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
                _animator.SetTrigger(AnimatorPlayerController.Params.Attack);
                AudioManager.Instance.PlayingSwordAttackSound();

                Collider2D[] enemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _enemyLayerMask);

                if (enemies.Length != 0)
                {
                    for (int i = 0; i < enemies.Length; i++)
                    {
                        enemies[i].GetComponent<Enemy>().TakeDamage(_damage);
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

    private void Heal()
    {
        float healthPoints;
        int potionUsed = -1;

        healthPoints = _health.HealthPoints;
        healthPoints += _healingPotion.Healing();
        _health.SetHealthPoints(healthPoints);

        //EventHealthHasChanged?.Invoke();

        _inventory.ChangeNumberOfPotions(potionUsed);
        _particleHealing.Play();
        AudioManager.Instance.PlayingPotionUsedClip();
    }

    public void TakeDamage(float damage)
    {
        float healthPoints;

        if (damage >= 0)
        {
            healthPoints = _health.HealthPoints;
            healthPoints -= damage;
            
            if (healthPoints <= 0)
            {
                _playerMovement.enabled = false;
                healthPoints = 0;
                _isDead = true;
                _animator.SetTrigger(AnimatorPlayerController.Params.Dead);
            }

            _animator.SetTrigger(AnimatorPlayerController.Params.Hitting);
            //EventHealthHasChanged?.Invoke();

            _health.SetHealthPoints(healthPoints);
        }        
    }    
}

public static class AnimatorPlayerController
{
    public static class Params 
    { 
        public const string Dead = nameof(Dead);
        public const string Hitting = nameof(Hitting);
        public const string Attack = nameof(Attack);
        public const string IsGround = nameof(IsGround);
        public const string IsRunning = nameof(IsRunning);
    }
}
