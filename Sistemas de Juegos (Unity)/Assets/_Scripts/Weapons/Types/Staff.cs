using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : Weapon
{
    [ContextMenu("Debug Attack")]
    public override void WeaponAttack()
    {
        GameObject spell = Instantiate(WeaponStats.WeaponSpellPrefab, ShootPoint.transform.position, ShootPoint.transform.rotation);
        spell.GetComponent<MagicProjectile>().SetOwner(this);
    }
}
