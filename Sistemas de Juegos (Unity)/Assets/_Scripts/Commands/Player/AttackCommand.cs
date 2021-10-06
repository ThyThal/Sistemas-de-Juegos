using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCommand : ICommand
{
    private Weapon _weapon;

    public AttackCommand(Weapon weapon)
    {
        _weapon = weapon;
    }

    public void Do()
    {
        _weapon.WeaponAttack();
    }

    public void ChangeWeapon(Weapon weapon)
    {
        _weapon = weapon;
    }
}
