using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : Character
{
    //[SerializeField] private GameObject _dieParticles;
    [SerializeField] private Animator _animator;
    [SerializeField] private int _dieAnimatorID;
    
    private bool _isDead;
    public Animator Animator => _animator;


    private void Awake()
    {
        _dieAnimatorID = Animator.StringToHash("Death");
    }

    public void TriggerAnimatorDie()
    {
        _animator.SetTrigger(_dieAnimatorID);
    }

    private void Despawn()
    {

    }

    private void TriggerAnimatorAttack()
    {

    }
}
