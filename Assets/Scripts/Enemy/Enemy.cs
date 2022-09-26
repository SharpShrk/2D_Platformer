using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject _blast;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private LayerMask _playerLayerMask;
    [SerializeField] private Transform _attackZone;
    [SerializeField] private Player _player;

    private float _attackZoneRange = 8f;
    private float _attackCooldown = 4f;
    private float _attackTimer = 1f;
    private float _jumpTimer = 1f;
    private float _jumpCooldown = 3f;
    private float _jumpHeight = 400f;
    private float _timeToDestroy = 1.5f;
    private bool _isPlayerInAttackZone;
    private bool _isDrawingModGizmos;
    private bool _isDead;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private Health _health;

    private void Awake()
    {
        _isDead = false;
        _isPlayerInAttackZone = false;
        _isDrawingModGizmos = true;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _health = GetComponent<Health>();
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