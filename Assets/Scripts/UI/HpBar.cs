using UnityEngine;

[RequireComponent(typeof(Health))]
public class HpBar : MonoBehaviour
{
    [SerializeField] private Transform _fill;
    private Health _health;

    private int _previousHP; // Previous health value
    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _health.OnHealthChanged += UpdateHPBar;
        _previousHP = _health.CurrentHp; 
    }

    private void Start()
    {
        _previousHP = _health.CurrentHp;
    }

    private void OnDisable()
    {
        _health.OnHealthChanged -= UpdateHPBar;
    }

    private void UpdateHPBar(int currentHP, int maxHP)
    {
        if (currentHP == _previousHP)
            return;

        if (_fill != null)
        {
            float fillAmount = Mathf.Clamp01((float)currentHP / maxHP);

            _fill.localScale = new Vector3(fillAmount, _fill.localScale.y, _fill.localScale.z);

            // Shifting an object to create the illusion that the HP bar is decreasing on only one side
            float fillWidth = _fill.GetComponent<SpriteRenderer>().bounds.size.x;
            float newXPosition = -(1f - fillAmount) * (fillWidth / 2f) - 0.1f;
            _fill.localPosition = new Vector3(newXPosition, _fill.localPosition.y, _fill.localPosition.z);
        }

        _previousHP = currentHP;
    }
}
