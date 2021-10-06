using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAudio : MonoBehaviour
{
    [Header("Audio Components")]
    [SerializeField] private CharacterAudioIdle _characterAudioIdle;
    [SerializeField] private CharacterAudioAttack _characterAudioAttack;
    [SerializeField] private CharacterAudioWalk _characterAudioWalk;
    [SerializeField] private CharacterAudioHurt _characterAudioHurt;

    public CharacterAudioAttack AudioAttack => _characterAudioAttack;
    public CharacterAudioIdle AudioIdle => _characterAudioIdle;
    public CharacterAudioHurt AudioHurt => _characterAudioHurt;
    public CharacterAudioWalk AudioWalk => _characterAudioWalk;

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
