using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy/StatsForEnemy")]
public class Enemy : Stats
{
    [Header("Enemy specific stats")]
    [SerializeField] private float _visibilityRange;
    [SerializeField] private EnemyObjectPool _nameEnemyIbObjectPool;

    public float VisibilityRange => _visibilityRange;
    public EnemyObjectPool NameEnemyInOjbectPool => _nameEnemyIbObjectPool;
}
