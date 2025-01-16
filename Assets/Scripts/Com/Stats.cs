using UnityEngine;

public abstract class Stats: ScriptableObject
{
    [SerializeField] protected int _maxHp;
    [SerializeField] protected float _movementSpeed;
    [SerializeField] protected float _rangeAttack;

    public int MaxHp => _maxHp;
    public float MovementSpeed => _movementSpeed;
    public float RangeAttack => _rangeAttack;  
}