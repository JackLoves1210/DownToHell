using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelArsenal;
public class BulletBot : Bullet
{
    public override void OnInit(Character character, Vector3 target)
    {
        base.OnInit(character, target);
        projectileParticle = SimplePool.Spawn<BulletMissile>(PoolType.BulletMissileYellow, TF.position, Quaternion.identity);
        projectileParticle.gameObject.GetComponent<VoxelSoundSpawn>().Play();
        projectileParticle.gameObject.GetComponent<ParticleSystem>().Play();
        projectileParticle.TF.parent = TF;
        this.character = character;
        TF.forward = (target - TF.position).normalized;
        counterTime.Start(OnDespawnAll, timeAlive);
        isRunning = true;
    }

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
        if (other.CompareTag(Constant.TAG_PLAYER))
        {
            Player player = Cache.GetPlayer(other);
            player.DealDamage(player.gameObject,damage);
            player.healthBar.UpDateHealthBar(player.maxHp, player.hp);
            ParticlePool.Play(ParticleType.Hit_1, TF.position, Quaternion.identity);
            OnDespawn();
            DespawnSFX();
        }
        
    }
}
