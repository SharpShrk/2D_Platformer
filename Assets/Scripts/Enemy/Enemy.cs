using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(EnemyAttack))]

public class Enemy : MonoBehaviour
{
    [SerializeField] private Player _player;

    private float _jumpTimer = 1f;
    private float _jumpCooldown = 3f;
    private float _jumpHeight = 300f;
    private float _timeToDestroy = 1.5f;
    private bool _isDead;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private Health _health;
    private EnemyAttack _enemyAttack;

    public bool IsDead => _isDead;

    private void Awake()
    {
        _isDead = false;        
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _health = GetComponent<Health>();
        _enemyAttack = GetComponent<EnemyAttack>();
    }

    private void Update()
    {
        if (_isDead == false)
        {
            Jump();
        }
    }

    public void TakeDamage(float damage)
    {
        float healthPoints;
        healthPoints = _health.HealthPoints;
        healthPoints -= damage;

        _animator.SetTrigger(AnimatorEnemyController.Params.Damaged);

        if (healthPoints <= 0)
        {
            _isDead = true;
            _animator.SetTrigger(AnimatorEnemyController.Params.Dead);
            Destroy(gameObject, _timeToDestroy);
        }

        _health.SetHealthPoints(healthPoints);
    }

    private void Jump()
    {
        if (_jumpTimer <= 0f && _enemyAttack.IsPlayerInAttackZone == true && _player.IsDead == false)
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