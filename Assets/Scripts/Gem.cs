using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    [SerializeField] private GameObject _player;

    private AudioManager _audioManager;
    private Inventory _inventory;
    private int _count = 1;

    private void Awake()
    {
        _inventory = _player.GetComponent<Inventory>();
        _audioManager = _player.GetComponent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            _audioManager.PlayingGemClip();
            _inventory.ChangeNumberOfGems(_count);
            Destroy(gameObject);
        }
    }
}
