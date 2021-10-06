using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BreakableObject : MonoBehaviour, IDamagable
{
    [Header("Breakable Object")]
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _currentHealth;
    [SerializeField] private GameObject _destroyParticles;
    [SerializeField] private UnityEvent _triggerDieEvent;
    [SerializeField] private Collider _hitbox;

    [SerializeField] private bool _isBreakable = false;
    public bool IsBreakable {get { return _isBreakable; } set { _isBreakable = value; } }

    private void Start()
    {
        Heal(Mathf.Infinity);
    }

    public void TakeDamage(float damage)
    {
        if (_isBreakable == true)
        {
            _currentHealth -= damage;
            //_characterAudioHurt.PlayAudio();

            if (_currentHealth <= 0) { Die(); _triggerDieEvent.Invoke(); }
        }

    }
    public void Die()
    {
        _destroyParticles = Instantiate(_destroyParticles, transform.position, Quaternion.identity);
        Destroy(_destroyParticles, 2f);
        _hitbox.enabled = false;
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        _hitbox.enabled = false;
    }

    public void Heal(float amount)
    {
        _currentHealth = Mathf.Clamp(amount, 0, _maxHealth);
    }
    public void Revive()
    {
        throw new System.NotImplementedException();
    }
}
