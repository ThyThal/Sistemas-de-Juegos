using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : Weapon
{
    [ContextMenu("Debug Attack")]
    public override void WeaponAttack()
    {
        //GameObject spell = Instantiate(WeaponStats.WeaponSpellPrefab, ShootPoint.transform.position, ShootPoint.transform.rotation);
        GameObject spell = GameManager.Instance.PlayerBulletsPool.GetFromPool();
        spell.transform.position = ShootPoint.transform.position;
        spell.transform.rotation = ShootPoint.transform.rotation;

        //spell.transform.SetParent(transform);
        var projectile = spell.GetComponent<MagicProjectile>();
        projectile.SetOwner(this);
        projectile.Activate();

    }
}
