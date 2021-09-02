using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAudio : MonoBehaviour
{
    [Header("Audio Components")]
    [SerializeField] public CharacterAudioIdle _characterAudioIdle;
    [SerializeField] public CharacterAudioAttack _characterAudioAttack;
    [SerializeField] public CharacterAudioWalk _characterAudioWalk;
    [SerializeField] public CharacterAudioHurt _characterAudioHurt;

    #region // Audio Component Classes.
    [System.Serializable]
    public class CharacterAudioIdle
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip[] _audioClips;

        public void PlayAudio()
        {
            _audioSource.PlayOneShot(_audioClips[Random.Range(0, _audioClips.Length)]);
        }
    }
    [System.Serializable]
    public class CharacterAudioAttack
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip[] _audioClips;

        public void PlayAudio()
        {
            _audioSource.PlayOneShot(_audioClips[Random.Range(0, _audioClips.Length)]);
        }
    }
    [System.Serializable]
    public class CharacterAudioWalk
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip[] _audioClips;

        public void PlayAudio()
        {
            _audioSource.PlayOneShot(_audioClips[Random.Range(0, _audioClips.Length)]);
        }
    }
    [System.Serializable]
    public class CharacterAudioHurt
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip[] _audioClips;

        public void PlayAudio()
        {
            _audioSource.PlayOneShot(_audioClips[Random.Range(0, _audioClips.Length)]);
        }
    }
    #endregion // Audio Component Classes.
}
