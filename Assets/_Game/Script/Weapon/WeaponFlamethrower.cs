using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFlamethrower : Weapon
{
  
    public override void Shooting(Character character, Vector3 target) 
    {
        Vector3 pos = (target - character.TF.position).normalized *(1 + size)  + character.TF.position ;
        base.Shooting(character, target);
        Bullet bullet = SimplePool.Spawn<Bullet>((PoolType)bulletType, pos, Quaternion.identity);
        bullet.damage = damageWeapon;
        bullet.OnInit(character, target);
        //Vector3 upScaleSize = bullet.defaultScaleSize + bullet.transform.localScale * 0.5f;
        //bullet.transform.localScale = upScaleSize;
    }
}
