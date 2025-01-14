using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class HpBar : MonoBehaviour
{
    [SerializeField] private Transform _fill;
    private Health _health;

    private void Start()
    {
        _health = GetComponent<Health>();
        _health.OnHealthChanged += UpdateHPBar;
    }

    private void UpdateHPBar(int currentHP, int maxHP)
    {
        if (_fill != null)
        {
            float fillAmount = Mathf.Clamp01((float)currentHP / maxHP);

            _fill.localScale = new Vector3(fillAmount, _fill.localScale.y, _fill.localScale.z);

            // Сдвиг объекта, чтобы создавалась иллюзия, что хп бар уменьшается только с одной стороны
            float fillWidth = _fill.GetComponent<SpriteRenderer>().bounds.size.x;
            float newXPosition = -(1f - fillAmount) * (fillWidth / 2f) - 0.1f; 
            _fill.localPosition = new Vector3(newXPosition, _fill.localPosition.y, _fill.localPosition.z);
        }
    }
}