using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy
{
    [Header("Skeleton Information")]
    [SerializeField] private EnemyStats _enemyStats;
    [SerializeField] private int _health;
    [SerializeField] private int _damage;
    [SerializeField] private int _speed;

    private void Start()
    {
        CharacterData(_enemyStats.Health);
    }
}
