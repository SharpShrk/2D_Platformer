using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Player))]

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private Image _bar;
    [SerializeField] private Player _player;

    private float _coefficientHealthPoints = 0.01f;

    private void OnEnable()
    {
        _player.EventHealthHasChanged += SetHealthPonts;
    }

    private void OnDisable()
    {
        _player.EventHealthHasChanged -= SetHealthPonts;
    }

    void Start()
    {
        _player = GetComponent<Player>();
        _bar.fillAmount = _player.HealthPoints * _coefficientHealthPoints;
    }

    private void SetHealthPonts()
    {
        _bar.fillAmount = _player.HealthPoints * _coefficientHealthPoints;
    }
}
