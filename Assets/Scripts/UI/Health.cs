using System;
using System.Collections;
using UnityEngine;


public class Health : MonoBehaviour
{
    //Характеристики из ScriptableObject файла если он используется для этого юнита
    [SerializeField] Stats _stats;
    //Хп из инспектора, если SO не используется
    [SerializeField] private int  _hpFromInspector = 100;

    private int  _maxHp;
    private int _currentHp;

    public event Action<int, int> OnHealthChanged;

    void Start()
    {
        if (_stats != null)
        {
            _maxHp = _stats.MaxHp;
        }
        else
        {
            _maxHp = _hpFromInspector;
        }
        _currentHp = _maxHp;
        NotifyHealthChanged();
        StartCoroutine(TestDamage());
    }

    private void NotifyHealthChanged()
    {
        OnHealthChanged.Invoke(_currentHp, _maxHp);
        Debug.Log($"current = {_currentHp} max = {_maxHp}");
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
        Debug.Log("Временная заглушка...");
    }

    private IEnumerator TestDamage()
    {
       

        TakeDamage(20);

        yield return new WaitForSeconds(1f);
    }
}
