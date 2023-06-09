using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBot : Weapon
{
    public override void Shooting(Character character, Vector3 target)
    {
        base.Shooting(character, target);
        damageWeapon = character.baseDame;
        Bullet bullet = SimplePool.Spawn<Bullet>((PoolType)bulletType, character.TF.position, Quaternion.identity);
        bullet.damage = damageWeapon;
        bullet.OnInit(character, target);
    }
}
