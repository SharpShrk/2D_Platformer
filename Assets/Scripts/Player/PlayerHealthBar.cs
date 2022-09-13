using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Player))]

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private Image _bar;
    
    private Player _player;
    private float _coefficientHealthPoints;

    void Start()
    {
        _coefficientHealthPoints = 0.01f;
        _player = GetComponent<Player>();
    }

    void Update()
    {
        SetHealthPonts();
    }

    private void SetHealthPonts()
    {
        _bar.fillAmount = _player.HealthPoints * _coefficientHealthPoints;
    }
}
