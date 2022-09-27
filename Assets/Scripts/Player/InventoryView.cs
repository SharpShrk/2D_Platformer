using UnityEngine;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _numberOfPotionsText;
    [SerializeField] private GameObject _playerObject;

    private Player _player;
    private int _numberOfPotions;
    private int _score;
    private Inventory _inventory;

    private void Awake()
    {
        _player = _playerObject.GetComponent<Player>();
        _numberOfPotionsText.text = _numberOfPotions.ToString();
        _scoreText.text = _score.ToString();
        _inventory = _player.GetInventory;
    }

    private void Update()
    {
        TakeInventoryItems();
    }

    private void TakeInventoryItems()
    {
        _numberOfPotions = _inventory.NumberOfPotions;
        _score = _inventory.Score;

        _numberOfPotionsText.text = _numberOfPotions.ToString();
        _scoreText.text = _score.ToString();
    }
}
