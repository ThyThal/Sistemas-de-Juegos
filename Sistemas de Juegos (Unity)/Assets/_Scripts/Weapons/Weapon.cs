using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour, IEquipable, IWeapon
{
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private WeaponStats _weaponStats;
    [SerializeField] private bool _isEquipped = false;

    public bool IsEquipped => _isEquipped;
    public Transform ShootPoint => _shootPoint;
    public WeaponStats WeaponStats => _weaponStats;

    public void Equip()
    {
        _isEquipped = true;
        _shootPoint = GameManager.Instance.Player.ShootingPoint;
        GameManager.Instance.PlayerBulletsPool._originalPrefab = WeaponStats.WeaponSpellPrefab;
    }

    public void Unequip()
    {
        Destroy(this.gameObject);
    }

    public virtual void WeaponAttack()
    {

    }
}
