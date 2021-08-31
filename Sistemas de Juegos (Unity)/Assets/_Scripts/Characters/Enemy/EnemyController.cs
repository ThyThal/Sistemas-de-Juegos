using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Character
{
    [SerializeField] private Animator _animator;
    public Animator Animator => _animator;
}
