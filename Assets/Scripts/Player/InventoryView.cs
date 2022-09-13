using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _numberOfPotionsText;
    [SerializeField] private GameObject _player;

    private int _numberOfPotions;
    private int _score;
    private Inventory _inventory;

    private void Awake()
    {
        _numberOfPotionsText.text = _numberOfPotions.ToString();
        _scoreText.text = _score.ToString();
        _inventory = _player.GetComponent<Inventory>();
    }

    void Update()
    {
        GetInventoryItems();
    }

    private void GetInventoryItems()
    {
        _numberOfPotions = _inventory.NumberOfPotions;
        _score = _inventory.Score;

        _numberOfPotionsText.text = _numberOfPotions.ToString();
        _scoreText.text = _score.ToString();
    }
}
