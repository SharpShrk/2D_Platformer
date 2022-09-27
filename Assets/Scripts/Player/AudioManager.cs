using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioClip _gemClip;
    [SerializeField] private AudioClip _potionCollect;
    [SerializeField] private AudioClip _potionUsed;
    [SerializeField] private AudioClip _swordAttack;
    
    private AudioSource _audioSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayGemClip()
    {
        _audioSource.PlayOneShot(_gemClip);
    }

    public void PlayPotionCollectClip()
    {
        _audioSource.PlayOneShot(_potionCollect);
    }

    public void PlayPotionUsedClip()
    {
        _audioSource.PlayOneShot(_potionUsed);
    }

    public void PlaySwordAttackSound()
    {
        _audioSource.PlayOneShot(_swordAttack);
    }
}
