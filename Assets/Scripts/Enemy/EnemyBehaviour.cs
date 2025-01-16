using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private Enemy _enemyStats;

    private EnemyObjectPool _nameEnemyInObjectPool = EnemyObjectPool.Enemy;
    private float _visibilityRange = 50f;
    private float _moveSpeed = 5f;

    private void Awake()
    {
        if (_enemyStats != null)
        {
            _nameEnemyInObjectPool = _enemyStats.NameEnemyInOjbectPool;
            _visibilityRange = _enemyStats.VisibilityRange;
            _moveSpeed = _enemyStats.MovementSpeed;
        }
        else Debug.LogWarning($"Enemy {gameObject} dont have enemy stats from Enemy");
    }

    public void Die()
    {
        if (this.isActiveAndEnabled) ObjectPoolForEnemy.Instance.ReturnEnemy(_nameEnemyInObjectPool, gameObject);
    }
}
