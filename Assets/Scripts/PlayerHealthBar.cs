using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private Image _bar;

    private float _fill;

    void Start()
    {
        _fill = 1f;
    }

    void Update()
    {
        if (_fill > 1f)
        {
            _fill = 1f;
        }

        if (_fill < 0f)
        {
            _fill = 0f;
        }

        _bar.fillAmount = _fill;
    }
}
