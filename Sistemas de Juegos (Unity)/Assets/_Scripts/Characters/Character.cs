using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using EZCameraShake;

public class Character : MonoBehaviour, IDamagable
{
    [Header("Character Components")]
    [SerializeField] private bool isPlayer;
    [SerializeField] private GameObject _dieParticles;
    [SerializeField] private CharacterAudio _characterAudio;
    [SerializeField] private UnityEvent _triggerDieEvent;

    [Header("Character Stats")]
    [SerializeField] private float _maxLife;
    [SerializeField] private float _currentHealth;

    //==================== Properties ====================\\

    // Variables
    public float CurrentHealth => _currentHealth;
    public CharacterAudio CharacterAudio => _characterAudio;

    private void Awake()
    {
        _characterAudio = GetComponent<CharacterAudio>();
    }

    //==================== IDamagable ====================\\
    public virtual void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        //_characterAudioHurt.PlayAudio();

        if (isPlayer) {CameraShaker.Instance.ShakeOnce(5f, 4f, 0.1f, 1f); }
        if (CurrentHealth <= 0) { Die(); _triggerDieEvent.Invoke(); }
    }
    public void Heal(float amount)
    {
        _currentHealth = Mathf.Clamp(amount, 0, _maxLife);
    }
    public void Die()
    {
        _dieParticles = Instantiate(_dieParticles, transform.position, Quaternion.identity);
        Destroy(_dieParticles, 2f);
        var collider = GetComponent<Collider>();
        if (collider != null)
        {
            GetComponent<Collider>().enabled = false;
        }
        //this.gameObject.SetActive(false);
    }

    public void Revive()
    {
        Heal(_currentHealth * 0.25f);
        this.gameObject.SetActive(true);
        GetComponent<Collider>().enabled = false;
    }

    public void CharacterData(int health)
    {
        _maxLife = health;
        Heal(health);
    }
}
