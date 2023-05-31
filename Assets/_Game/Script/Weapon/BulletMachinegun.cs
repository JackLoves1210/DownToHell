using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelArsenal;
public class BulletMachinegun : Bullet
{
    public override void OnInit(Character character, Vector3 target)
    {
        base.OnInit(character, target);
        projectileParticle = SimplePool.Spawn<BulletMissile>(PoolType.BulletMissileYellow,TF.position,Quaternion.identity);
        projectileParticle.gameObject.GetComponent<VoxelSoundSpawn>().Play();
        projectileParticle.gameObject.GetComponent<ParticleSystem>().Play();
        projectileParticle.TF.parent = TF;

        this.character = character;
        TF.forward = (target - TF.position).normalized;
        counterTime.Start(OnDespawnAll, timeAlive);
        isRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        counterTime.Execute();
        if (isRunning)
        {
            TF.Translate(TF.forward * moveSpeed * Time.deltaTime, Space.World);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAG_CHARACTER))
        {
            OnDespawn();
            Bot bot = Cache.GetBot(other);
            bot.DealDamage(bot.gameObject,damage);
            LevelManager.Ins.player.HealHp((damage * LevelManager.Ins.player.lifeSteal / (float)100));
            ParticlePool.Play(ParticleType.BulletExplosionYellow,   bot.TF.position, Quaternion.identity);
            DespawnSFX();
        }
       
    }
}
