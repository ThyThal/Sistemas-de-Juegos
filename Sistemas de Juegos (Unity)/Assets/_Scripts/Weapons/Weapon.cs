using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour, IEquipable, IWeapon
{
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private WeaponStats _weaponStats;

    public Transform ShootPoint => _shootPoint;
    public WeaponStats WeaponStats => _weaponStats;

    public void Equip()
    {
        _shootPoint = GameManager.Instance.Player.ShootPoint;
    }

    public void Unequip()
    {
        Destroy(this.gameObject);
    }

    public virtual void WeaponAttack()
    {

    }
}
