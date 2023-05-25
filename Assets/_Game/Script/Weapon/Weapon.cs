using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : GameUnit
{
    public float timeBettwenShoot = 0.5f;
    public GameObject weaponModel;
    public BulletType bulletType;
    public float damageWeapon;
    public bool isCanAttack;
    public float size;
    public int powerUps;
    public void Start()
    {
        isCanAttack = true;
    }

    public void OnRest()
    {
        powerUps = 1;
        timeBettwenShoot = 0.4f;
        size = 0;
        isCanAttack = true;
    }

    public void SetAttack(bool attack)
    {
        isCanAttack = attack;
    }
    private void OnAttack()
    {
        SetAttack(true);
    }
    public virtual void Shooting(Character character, Vector3 target)
    {
        SetAttack(false);
        Invoke(nameof(OnAttack), timeBettwenShoot);
    }

}
