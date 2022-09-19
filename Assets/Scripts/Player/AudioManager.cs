using System.Collections;
using System.Collections.Generic;
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

    public void PlayingGemClip()
    {
        _audioSource.PlayOneShot(_gemClip);
    }

    public void PlayingPotionCollectClip()
    {
        _audioSource.PlayOneShot(_potionCollect);
    }

    public void PlayingPotionUsedClip()
    {
        _audioSource.PlayOneShot(_potionUsed);
    }

    public void PlayingSwordAttackSound()
    {
        _audioSource.PlayOneShot(_swordAttack);
    }
}
