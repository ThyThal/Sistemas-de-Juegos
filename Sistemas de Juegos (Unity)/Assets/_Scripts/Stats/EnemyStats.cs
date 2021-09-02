using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Stats", menuName = "Stats/Enemy Stats")]
public class EnemyStats : ScriptableObject
{
    [SerializeField] private string _name = "Enemy Name";
    [SerializeField] private int _health;
    [SerializeField] private int _damage;
    [SerializeField] private int _speed;

    public int Health => _health;
    public int Damage => _damage;
    public int Speed => _speed;

}
