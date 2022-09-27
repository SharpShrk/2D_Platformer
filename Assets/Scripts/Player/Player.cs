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

    private Animator _animator;
    private Inventory _inventory;
    private PlayerMovement _playerMovement;
    private Health _health;
    private float _damage = 10f;
    private float _attackRange = 0.5f;
    private float _attackColdown = 0.5f;
    private float _attackTimer = 0f;
    private bool _isDrawingModGizmos;
    private bool _isDead = false;

    public bool IsDead => _isDead;

    public Inventory GetInventory => _inventory;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _inventory = GetComponent<Inventory>();
        _playerMovement = GetComponent<PlayerMovement>();
        _health = GetComponent<Health>();
        _isDrawingModGizmos = true;
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
                AudioManager.Instance.PlaySwordAttackSound();

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

        _inventory.ChangeNumberOfPotions(potionUsed);
        _particleHealing.Play();
        AudioManager.Instance.PlayPotionUsedClip();
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

            _health.SetHealthPoints(healthPoints);
        }        
    }    
}
