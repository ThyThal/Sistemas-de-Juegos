using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : EnemyController
{
    [SerializeField] private Vector2 _idleVariationTimer;
    [SerializeField] private Vector2 _idleSoundTimer;

    [SerializeField] private AudioSource _idleSource;
    [SerializeField] List<AudioClip> _peckSounds;
    [SerializeField] List<AudioClip> _idleSounds;
    private float _idleVariationTimerOG;
    private float _idleSoundTimerOG;

    // Cache Animator.
    [SerializeField] private List<int> _cachedAnimator;

    private void Start()
    {
        SelectRandomTimeIdle();
        SelectRandomTimeAnimation();
        _cachedAnimator.Add(Animator.StringToHash("Eat"));
        _cachedAnimator.Add(Animator.StringToHash("Turn Head"));
    }

    private void Update()
    {
        PlayIdleSound();
        IdleAnimationVariation();
    }

    private void PlayIdleSound()
    {
        if (_idleSoundTimerOG > 0)
        {
            _idleSoundTimerOG -= Time.deltaTime;
        }

        else
        {
            SelectRandomTimeIdle();
            _idleSource.PlayOneShot(_idleSounds[Random.Range(0, _idleSounds.Count)]);
        }
    }

    private void IdleAnimationVariation()
    {
        if (_idleVariationTimerOG > 0)
        {
            _idleVariationTimerOG -= Time.deltaTime;
        }

        else
        {
            SelectRandomTimeAnimation();
            Animator.SetTrigger(_cachedAnimator[Random.Range(0, _cachedAnimator.Count)]);
            _idleSource.PlayOneShot(_peckSounds[Random.Range(0, _peckSounds.Count)]);
        }
    }
    private void SelectRandomTimeAnimation()
    {
        _idleVariationTimerOG = Random.Range(_idleVariationTimer.x, _idleVariationTimer.y);
    }

    private void SelectRandomTimeIdle()
    {
        _idleSoundTimerOG = Random.Range(_idleSoundTimer.x, _idleSoundTimer.y);
    }
}
