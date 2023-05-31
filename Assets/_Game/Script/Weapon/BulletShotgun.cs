using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelArsenal;
public class BulletShotgun : Bullet
{
    public override void OnInit(Character character, Vector3 target)
    {
        base.OnInit(character, target);
        projectileParticle = SimplePool.Spawn<BulletMissile>(PoolType.BulletMissilePink, TF.position, Quaternion.identity);
        projectileParticle.gameObject.GetComponent<VoxelSoundSpawn>().Play();
        projectileParticle.gameObject.GetComponent<ParticleSystem>().Play();
        projectileParticle.transform.parent = transform;
        // this.character = character;
        //TF.forward = (target - character.TF.position).normalized;
        counterTime.Start(OnDespawnAll, timeAlive);
        //isRunning = true;
    }

    void Update()
    {
        counterTime.Execute();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAG_CHARACTER))
        {
            OnDespawn();
            Bot bot = Cache.GetBot(other);
            bot.DealDamage(bot.gameObject,damage);
            LevelManager.Ins.player.HealHp((damage * LevelManager.Ins.player.lifeSteal / (float)100));
            ParticlePool.Play(ParticleType.BulletExplosionPink, bot.TF.position, Quaternion.identity);
            SimplePool.Despawn(projectileParticle);
        }
        //  ParticlePool.Play(ParticleType.Hit, transform.position, Quaternion.identity);

    }
}
