using UnityEngine;

public class HealingPotion : MonoBehaviour
{
    [SerializeField] private GameObject _objectOfHealing;

    private Player _player;
    private float _healthPointsRestore;
    private Inventory _inventory;
    private int _numberOfPotions = 1;

    private void Start()
    {
        _healthPointsRestore = 20f;
        _player = _objectOfHealing.GetComponent<Player>();
        _inventory = _player.GetInventory;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Player>(out Player player))
        {
            AudioManager.Instance.PlayPotionCollectClip();
            _inventory.ChangeNumberOfPotions(_numberOfPotions);
            Destroy(gameObject);
        }
    }

    public float Healing()
    {
        return _healthPointsRestore;
    }
}
