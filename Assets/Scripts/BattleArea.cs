using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class BattleArea : MonoBehaviour
{
    [SerializeField] private GameObject _enemyHealthPointBar;
    [SerializeField] private GameObject _backgroundMusic;

    private AudioSource _audioSourceBattle;
    private AudioSource _audioSourceBackground;

    private void Awake()
    {
        _enemyHealthPointBar.SetActive(false); 
        _audioSourceBattle = GetComponent<AudioSource>();
        _audioSourceBackground = _backgroundMusic.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            _enemyHealthPointBar.SetActive(true);
            _audioSourceBackground.Stop();
            _audioSourceBattle.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            _enemyHealthPointBar.SetActive(false);
            _audioSourceBattle.Stop();
            _audioSourceBackground.Play();
        }
    }
}
