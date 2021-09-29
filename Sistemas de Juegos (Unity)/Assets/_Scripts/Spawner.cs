using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner<T> : IDamagable, IFactory<T> where T : MonoBehaviour
{
    private int _maxHealth;
    private int _currentHealth;

    public T Create(T prefab)
    {
        return GameObject.Instantiate(prefab);
    }

    public void Die()
    {
        throw new System.NotImplementedException();
    }
    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        // Play Audio
    }

    public void Heal(float amount)
    {
        throw new System.NotImplementedException();
    }
    public void Revive()
    {
        throw new System.NotImplementedException();
    }
}
