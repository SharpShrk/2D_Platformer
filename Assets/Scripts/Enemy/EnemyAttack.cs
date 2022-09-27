using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Enemy))]

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private Transform _attackZone;
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _blast;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private LayerMask _playerLayerMask;

    private Enemy _enemy;
    private Animator _animator;
    private float _attackZoneRange = 8f;
    private float _attackCooldown = 4f;
    private bool _isDrawingModGizmos;
    private bool _isPlayerInAttackZone;

    public bool IsPlayerInAttackZone => _isPlayerInAttackZone;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _animator = GetComponent<Animator>();
        _isDrawingModGizmos = true;
        _isPlayerInAttackZone = false;
}

    private void Update()
    {
        if (_enemy.IsDead == false)
        {
            StartCoroutine(AttackCooldown());
        }
        else
        {
            StopCoroutine(AttackCooldown());
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

    private void Attack()
    {
        _isPlayerInAttackZone = GetPlayerInAttackZone();

        if (_isPlayerInAttackZone == true && _player.IsDead == false)
        {
            _animator.SetTrigger(AnimatorEnemyController.Params.Attack);
            Instantiate(_blast, _firePoint.transform);
        }
        else if (_player.IsDead == true)
        {
            _animator.SetBool(AnimatorEnemyController.Params.PlayerDead, true);
        }
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

    private IEnumerator AttackCooldown()
    {
        var attackCooldown = new WaitForSeconds(_attackCooldown);

        Attack();

        yield return attackCooldown;
    }
}
