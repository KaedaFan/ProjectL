using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Player/StatsForPlayer")]
public class PlayerStats : Stats
{
    [Header("Player specific stats")]
    [SerializeField] private EffectObjectPool _nameProjectile;
    [SerializeField] private float _throwForce;
    [SerializeField] private int _damageAmount;

    public EffectObjectPool NameProjectile => _nameProjectile;
    public float ThrowForce => _throwForce;
    public int DamageAmount => _damageAmount;

}
