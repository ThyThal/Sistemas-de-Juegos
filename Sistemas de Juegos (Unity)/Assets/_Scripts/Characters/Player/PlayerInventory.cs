using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [Header("Weapon Holders")]
    [SerializeField] private Transform _staffHolder;
    [SerializeField] private Transform _wandHolder;

    [Header("Player Information")]
    [SerializeField] private Weapon _currentWeapon;

    public Weapon CurrentWeapon => _currentWeapon;

    public void LootWeapons(GameObject weapon)
    {

        _currentWeapon = weapon.GetComponent<Weapon>();
        _currentWeapon.Equip();
        _currentWeapon.transform.parent = _staffHolder;
        EquipWeaponVisual();
    }

    public void RemoveWeapon()
    {
        _currentWeapon = null;
    }

    private void EquipWeaponVisual()
    {
        _currentWeapon.GetComponent<Weapon>().Equip();
        _currentWeapon.transform.SetParent(_staffHolder);
        _currentWeapon.transform.localPosition = Vector3.zero;
        _currentWeapon.transform.localRotation = Quaternion.identity;
        _currentWeapon.transform.localScale = Vector3.one;
    }
}
