using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Health))]

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _bar;

    private Health _health;

    private float _coefficientHealthPoints = 0.01f;

    private void OnEnable()
    {
        _health.EventHealthHasChanged += SetHealthPonts;
    }

    private void OnDisable()
    {
        _health.EventHealthHasChanged -= SetHealthPonts;
    }

    private void Start()
    {
        _health = GetComponent<Health>();
        _bar.fillAmount = _health.HealthPoints * _coefficientHealthPoints;
    }

    private void SetHealthPonts()
    {
        _bar.fillAmount = _health.HealthPoints * _coefficientHealthPoints;
    }
}
