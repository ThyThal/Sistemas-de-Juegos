using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerInventory _playerInventory;

    private int _playerMovementAnimationID;
    private int _playerAttackAnimationID;

    private void Start()
    {
        _playerMovementAnimationID = Animator.StringToHash("Movement");
        _playerAttackAnimationID = Animator.StringToHash("Attack");
    }

    public void UpdateMovementAnimation(float movementBlendValue)
    {
        _animator.SetFloat(_playerMovementAnimationID, movementBlendValue);
    }

    public void PlayAttackAnimation()
    {
        if (_playerInventory.CurrentWeapon != null)
        {
            _animator.SetTrigger(_playerAttackAnimationID);
        }
    }
}
