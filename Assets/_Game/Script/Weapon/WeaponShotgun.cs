using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShotgun : Weapon
{
    public int numberOfBullet;
    public float Angle = 30;
    public override void OnRest()
    {
        base.OnRest();
        timeBettwenShoot = 0.4f;
        timeBulletAlive = 0.4f;
        size = 0;
    }
    public override void Shooting(Character character, Vector3 target)
    {
        base.Shooting(character, target);
        Vector3 shootDirection = (target - character.TF.position).normalized;  // Hướng bắn chính
        float spreadAngle = -Angle;
        for (int i = 0; i < numberOfBullet; i++)
        {
            spreadAngle += Angle / 2; // Góc phân tán ngẫu nhiên
            Quaternion spreadRotation = Quaternion.Euler(0f, spreadAngle, 0f); // Quay hướng phân tán

            Vector3 bulletDirection = spreadRotation * shootDirection; // Hướng bắn của viên đạn
            Bullet bullet = SimplePool.Spawn<Bullet>((PoolType)bulletType, character.TF.position, Quaternion.identity);
            bullet.damage = damageWeapon;
            bullet.timeAlive = timeBulletAlive;
            bullet.OnInit(character, target);
            bullet.GetComponent<Rigidbody>().velocity = bulletDirection * bullet.moveSpeed; //, ForceMode.Impulse);
        }
    }
}
