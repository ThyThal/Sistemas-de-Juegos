using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private static LTDescr _delay;
    [SerializeField] private LootableReward _reward;
    [SerializeField] public Weapon _weapon;
    [SerializeField] private string _header;
    [SerializeField] private string _rarity;
    [SerializeField] private string _description;
    [SerializeField] private string _damage;
    [SerializeField] private string _speed;

    private void Start()
    {
        if (_reward != null)
        {
            _weapon = _reward.Reward.GetComponent<Weapon>();
        }

        _header = _weapon.WeaponStats.WeaponName;
        _rarity = _weapon.WeaponStats.WeaponRarity;
        _description = _weapon.WeaponStats.WeaponDescription;
        _damage = $"Damage: {_weapon.WeaponStats.WeaponDamage}";
        _speed = $"Speed: {_weapon.WeaponStats.WeaponSpellSpeed}";
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_weapon.IsEquipped == false)
        {
            _delay = LeanTween.delayedCall(0.5f, () =>
            {
                TooltipSystem.Show(_description, _rarity, _damage, _speed, _header);
            });
        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        LeanTween.cancel(_delay.uniqueId);
        TooltipSystem.Hide();
    }

    public void OnMouseEnter()
    {
        if (_weapon.IsEquipped == false)
        {
            _delay = LeanTween.delayedCall(0.5f, () =>
            {
                TooltipSystem.Show(_description, _rarity, _damage, _speed, _header);
            });
        }
    }

    public void OnMouseExit()
    {
        LeanTween.cancel(_delay.uniqueId);
        TooltipSystem.Hide();
    }
}
