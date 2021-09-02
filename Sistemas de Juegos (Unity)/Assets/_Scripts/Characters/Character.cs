using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IDamagable
{
    [Header("Character Components")]
    [SerializeField] private GameObject _dieParticles;
    [SerializeField] private CharacterAudio _characterAudio;

    [Header("Character Stats")]
    [SerializeField] private float _maxLife;
    [SerializeField] private float _currentHealth;

    //==================== Properties ====================\\

    // Variables
    public float CurrentHealth => _currentHealth;

    //==================== IDamagable ====================\\
    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        //_characterAudioHurt.PlayAudio();

        if (CurrentHealth <= 0) { Die(); }
    }
    public void Heal(float amount)
    {
        _currentHealth = Mathf.Clamp(amount, 0, _maxLife);
    }
    public void Die()
    {
        _dieParticles = Instantiate(_dieParticles, transform.position, Quaternion.identity);
        Destroy(_dieParticles, 2f);
        this.gameObject.SetActive(false);
    }
    public void Revive()
    {
        Heal(_currentHealth * 0.25f);
        this.gameObject.SetActive(false);
    }

    public void CharacterData(int health)
    {
        _maxLife = health;
        Heal(health);
    }
}
