using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Blast : MonoBehaviour
{
    [SerializeField] private GameObject _parentsObject;

    private float _speed;
    private float _damage;
    private float _timeToDestroy = 5f;
    private Rigidbody2D _rigidbody2D;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _speed = 5f;
        _damage = 10f;

        _rigidbody2D.velocity = transform.right * _speed;
        
        Physics.IgnoreCollision(_parentsObject.GetComponent<Collider>(), GetComponent<Collider>());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            player.TakeDamage(_damage);
            Destroy(gameObject);
        }

        Destroy(gameObject, _timeToDestroy);
    }
}
