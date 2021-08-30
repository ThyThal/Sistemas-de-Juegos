using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IDamagable
{
    [SerializeField] private float _maxLife = 100f;
    [SerializeField] private float _currentLife;

    float CurrentLife => _currentLife;


    private void Start()
    {
        _currentLife = _maxLife;
    }


    public void TakeDamage(float damage)
    {
        _currentLife -= damage;

        if (_currentLife <= 0)
        {
            CharacterDie();
        }
    }

    private void CharacterDie()
    {
        Destroy(this.gameObject);
    }
}
