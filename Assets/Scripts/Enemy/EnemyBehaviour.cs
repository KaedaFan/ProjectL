using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private Enemy _enemyStats;
    private EnemyState _enemyState;

    private EnemyObjectPool _nameEnemyInObjectPool = EnemyObjectPool.Enemy;
    private float _visibilityRange = 50f;
    private float _moveSpeed = 5f;
    private float _attackRange = 1f;

    private Transform _player;

    private void Awake()
    {
        if (_enemyStats != null)
        {
            _nameEnemyInObjectPool = _enemyStats.NameEnemyInOjbectPool;
            _visibilityRange = _enemyStats.VisibilityRange;
            _moveSpeed = _enemyStats.MovementSpeed;
            _attackRange = _enemyStats.RangeAttack;
        }
        else Debug.LogWarning($"Enemy {gameObject} dont have enemy stats from Enemy");
    }

    private void Start()
    {
        _player = PlayerBehaviour.Instance.gameObject.transform;
    }

    private void Update()
    {
        if (_player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, _player.position);

            if (distanceToPlayer <= _attackRange)
            {
                _enemyState = EnemyState.Attack;
            }
            else if (distanceToPlayer <= _visibilityRange)
            {
                _enemyState = EnemyState.Chase;
            }
            else
            {
                _enemyState = EnemyState.Idle;
            }

            switch (_enemyState)
            {
                case EnemyState.Idle:
                    Idle();
                    break;
                case EnemyState.Chase:
                    break;
                case EnemyState.Attack:
                    break;
                case EnemyState.Die:
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
        Debug.Log("Атака");
    }

    public void Die()
    {
        if (this.isActiveAndEnabled) ObjectPoolForEnemy.Instance.ReturnEnemy(_nameEnemyInObjectPool, gameObject);
    }
}
