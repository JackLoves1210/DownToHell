using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAcid : Bullet
{
    public override void OnInit(Character character, Vector3 target)
    {
        base.OnInit(character, target);
        // this.character = character;
        //TF.forward = (target - character.TF.position).normalized;
        counterTime.Start(OnDespawn, timeAlive);
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
            //OnDespawn();
            Bot bot = Cache.GetBot(other);
            bot.DealDamage(bot.gameObject,damage);
            LevelManger.Ins.player.HealHp((damage * LevelManger.Ins.player.lifeSteal / (float)100));
        }
        //  ParticlePool.Play(ParticleType.Hit, transform.position, Quaternion.identity);

    }
}
