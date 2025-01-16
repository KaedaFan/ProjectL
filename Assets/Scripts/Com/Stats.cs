using UnityEngine;

public abstract class Stats: ScriptableObject
{
    [SerializeField] protected int _maxHp;
    [SerializeField] protected float _movementSpeed;
    public int MaxHp => _maxHp;
    public float MovementSpeed => _movementSpeed;
}