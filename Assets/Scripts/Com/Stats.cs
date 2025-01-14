using UnityEngine;

public abstract class Stats: ScriptableObject
{
    [SerializeField] private int _maxHp;
    [SerializeField] private float _movementSpeed;
    public int MaxHp => _maxHp;
    public float MovementSpeed => _movementSpeed;
}