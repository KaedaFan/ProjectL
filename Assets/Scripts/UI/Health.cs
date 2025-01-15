using System;
using System.Collections;
using UnityEngine;


public class Health : MonoBehaviour
{
    //�������������� �� ScriptableObject ����� ���� �� ������������ ��� ����� �����
    [SerializeField] Stats _stats;
    //�� �� ����������, ���� SO �� ������������
    [SerializeField] private int  _hpFromInspector = 100;

    private int  _maxHp;
    private int _currentHp;
    public int CurrentHp => _currentHp;

    public event Action<int, int> OnHealthChanged;

    private void Awake()
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
        Debug.Log("��������� ��������...");
    }
}
