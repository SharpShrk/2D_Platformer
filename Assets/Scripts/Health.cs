using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _healthPoints;

    private float _maxHealthPoints = 100f;

    public event Action EventHealthHasChanged;

    public float HealthPoints => _healthPoints;

    public void SetHealthPoints(float _newHealthPoints)
    {
        _healthPoints = _newHealthPoints;

        if (_healthPoints >= _maxHealthPoints)
        {
            _healthPoints = _maxHealthPoints;
        }

        EventHealthHasChanged?.Invoke();
        Debug.Log("Здоровье изменилось");
    }
}
