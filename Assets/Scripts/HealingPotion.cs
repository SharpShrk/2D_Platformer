using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPotion : MonoBehaviour
{
    [SerializeField] private GameObject _player;

    private AudioManager _audioManager;
    private float _healthPointsRestore;
    private Inventory _inventory;
    private int _numberOfPotions = 1;

    private void Awake()
    {
        _healthPointsRestore = 20f;
        _inventory = _player.GetComponent<Inventory>();
        _audioManager = _player.GetComponent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Player>(out Player player))
        {
            _audioManager.PlayingPotionCollectClip();
            _inventory.ChangeNumberOfPotions(_numberOfPotions);
            Destroy(gameObject);
        }
    }

    public float Healing()
    {
        return _healthPointsRestore;
    }
}
