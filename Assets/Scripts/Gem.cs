using UnityEngine;

public class Gem : MonoBehaviour
{
    [SerializeField] private GameObject _playerObject;

    private Player _player;
    private Inventory _inventory;
    private int _count = 1;

    private void Start()
    {
        _player = _playerObject.GetComponent<Player>();
        _inventory = _player.GetInventory;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            AudioManager.Instance.PlayingGemClip();
            _inventory.ChangeNumberOfGems(_count);
            Destroy(gameObject);
        }
    }
}
