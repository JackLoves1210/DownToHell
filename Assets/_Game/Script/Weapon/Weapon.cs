using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : GameUnit
{
    public float timeBettwenShoot = 0.5f;
    public GameObject weaponModel;
    public BulletType bulletType;
    public int damageWeapon;
    //public bool isCanAttack;

    //public void SetAttack(bool attack)
    //{
    //    isCanAttack = attack;
    //}
    //private void OnAttack()
    //{
    //    SetAttack(true);
    //}
    public virtual void Shooting(Character character, Vector3 target)
    {
        
    }

}
