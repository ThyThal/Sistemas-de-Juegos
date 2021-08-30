using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsTrigger : MonoBehaviour
{
    [SerializeField] private PlayerInventory _playerInventory;

    public void TriggerAttack()
    {
        Debug.Log("Debug Animation Attack");
        if (_playerInventory.CurrentWeapon != null)
        {
            _playerInventory.CurrentWeapon.WeaponAttack();
        }    
    }
}
