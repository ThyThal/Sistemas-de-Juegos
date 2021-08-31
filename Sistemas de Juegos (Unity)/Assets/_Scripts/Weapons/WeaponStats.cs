using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon Stats", menuName = "Stats/Weapon Stats")]
public class WeaponStats : ScriptableObject
{
    [SerializeField] private string _weaponName = "Weapon Name";
    [SerializeField] private GameObject _weaponSpellPrefab;
    [SerializeField] private GameObject _rarityRadius;
    [SerializeField] private GameObject _rarityBeam;
    [SerializeField] private int _weaponDamage;
    [SerializeField] private int _weaponSpellSpeed;

    public GameObject RarityRadius => _rarityRadius;
    public GameObject RarityBeam => _rarityBeam;

    public string WeaponName => _weaponName;
    public GameObject WeaponSpellPrefab => _weaponSpellPrefab;
    public int WeaponDamage => _weaponDamage;
    public int WeaponSpellSpeed => _weaponSpellSpeed;
}
