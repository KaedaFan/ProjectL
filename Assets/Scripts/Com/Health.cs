using System;
using System.Collections;
using UnityEngine;


public class Health : MonoBehaviour
{
    [SerializeField] Stats _stats;

    private int  _maxHp = 100;
    private int _currentHp;
    public int CurrentHp => _currentHp;

    public event Action<int, int> OnHealthChanged;

    private void Awake()
    {
        if (_stats != null)
        {
            _maxHp = _stats.MaxHp;
        }

        _currentHp = _maxHp;
    }

    void Start()
    {
        NotifyHealthChanged();
    }

    private void NotifyHealthChanged()
    {
        OnHealthChanged?.Invoke(_currentHp, _maxHp);
    }

    public void TakeDamage(int damage)
    {
        _currentHp -= damage;
        if (_currentHp < 0) _currentHp = 0;

        NotifyHealthChanged();
        if (IsDie()) Die();
    }

    private bool IsDie()
    {
        if (_currentHp <= 0) return true;
        else return false;
    }

    private void Die()
    {
        if (gameObject.tag == "Player")
        {
            PlayerBehaviour playerBehaviour = gameObject.GetComponent<PlayerBehaviour>();
            if (playerBehaviour != null)
            {
                playerBehaviour.Die();
            }
        }
        else
        {
            EnemyBehaviour enemyBehaviour = gameObject.GetComponent<EnemyBehaviour>();
            if (enemyBehaviour != null)
            {
                enemyBehaviour.Die();
            }
        }
    }
}
