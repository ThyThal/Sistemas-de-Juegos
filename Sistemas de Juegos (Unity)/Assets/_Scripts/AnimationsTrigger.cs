using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsTrigger : MonoBehaviour
{
    [SerializeField] private PlayerInventory _playerInventory;

    public void TriggerAttack()
    {
        if (_playerInventory.CurrentWeapon != null)
        {
            _playerInventory.CurrentWeapon.WeaponAttack();
        }    
    }
}
