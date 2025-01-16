using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy/StatsForEnemy")]
public class Enemy : Stats
{
    [Header("Enemy specific stats")]
    [SerializeField] private float _visibilityRange;
    [SerializeField] private EnemyObjectPool _nameEnemyIbObjectPool;
    [SerializeField] private int _attackDamage;
    [SerializeField] [Tooltip("Number of attacks per second")] private float _attackSpeed;

    [Header("Item drops list")]
    [SerializeField] private List<ItemObjectPool> _itemDropsFromEnemy;

    public float VisibilityRange => _visibilityRange;
    public EnemyObjectPool NameEnemyInOjbectPool => _nameEnemyIbObjectPool;
    public int AttackDamage => _attackDamage;
    public float AttackSpeed => _attackSpeed;
    public List<ItemObjectPool> ItemDropsFromEnemy => _itemDropsFromEnemy;
}
