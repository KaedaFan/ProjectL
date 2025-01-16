using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private Enemy _enemyStats;
    private EnemyState _currentState;

    private EnemyObjectPool _nameEnemyInObjectPool = EnemyObjectPool.Enemy;
    private float _visibilityRange = 50f;
    private float _moveSpeed = 5f;
    private float _attackRange = 1f;
    private int _attackDamage = 10;
    private float _attackSpeed = 1f;

    private float _startVisibilityRange;

    private Transform _player;
    private bool _isAttacking = false;

    private void Awake()
    {
        if (_enemyStats != null)
        {
            _nameEnemyInObjectPool = _enemyStats.NameEnemyInOjbectPool;
            _visibilityRange = _enemyStats.VisibilityRange;
            _moveSpeed = _enemyStats.MovementSpeed;
            _attackRange = _enemyStats.RangeAttack;
            _attackDamage = _enemyStats.AttackDamage;
            _attackSpeed = _enemyStats.AttackSpeed;

            _startVisibilityRange = _visibilityRange;
        }
        else Debug.LogWarning($"Enemy {gameObject} doesn't have enemy stats from Enemy.");
    }

    private void Start()
    {
        _player = PlayerBehaviour.Instance.gameObject.transform;
    }

    private void OnEnable()
    {
        _currentState = EnemyState.Idle;
        _isAttacking = false;
    }

    private void Update()
    {
        if (_player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, _player.position);

            if (distanceToPlayer <= _attackRange)
            {
                if (!_isAttacking)
                {
                    _currentState = EnemyState.Attack;
                }
            }
            else if (distanceToPlayer <= _visibilityRange)
            {
                _currentState = EnemyState.Chase;
                _isAttacking = false;
            }
            else
            {
                _currentState = EnemyState.Idle;
                _isAttacking = false;
            }

            switch (_currentState)
            {
                case EnemyState.Idle:
                    _visibilityRange = _startVisibilityRange; 
                    Idle();
                    break;
                case EnemyState.Chase:
                    _visibilityRange += 10f; //So that the player could leave, but it was more difficult
                    Chase();
                    break;
                case EnemyState.Attack:
                    Attack();
                    break;
            }
        }
    }

    private void Idle()
    {
    }

    private void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, _player.position, _moveSpeed * Time.deltaTime);
    }

    private void Attack()
    {
        if (!_isAttacking)
        {
            _isAttacking = true;
            StartCoroutine(PerformAttack());
        }
    }

    private IEnumerator PerformAttack()
    {
        while (_isAttacking)
        {
            PlayerBehaviour.Instance.HealthPlayer.TakeDamage(_attackDamage);

            yield return new WaitForSeconds(_attackSpeed);

            float distanceToPlayer = Vector2.Distance(transform.position, _player.position);
            if (distanceToPlayer > _attackRange)
            {
                _isAttacking = false;
                _currentState = EnemyState.Chase;
            }
        }
    }

    public void Die()
    {
        if (this.isActiveAndEnabled)
        {
            ObjectPoolForEnemy.Instance.ReturnEnemy(_nameEnemyInObjectPool, gameObject);
        }
    }
}