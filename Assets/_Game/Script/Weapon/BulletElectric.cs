using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletElectric :  Bullet
{
    public float timeSendDame = 0.5f;
    private float timeSendDameCounter;

    public override void OnInit(Character character, Vector3 target)
    {
        base.OnInit(character, target);
        this.character = character;
        TF.forward = (target - TF.position).normalized;
        counterTime.Start(OnDespawn, timeAlive);
        isRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        counterTime.Execute();
        TF.position = Vector3.Lerp(TF.position, character.TF.position, Time.deltaTime * 10);

    }
    private void OnTriggerStay(Collider other)
    {
        timeSendDameCounter -= Time.deltaTime;
        if (other.CompareTag(Constant.TAG_CHARACTER) && timeSendDameCounter < 0)
        {
            timeSendDameCounter = timeSendDame;
            Bot bot = Cache.GetBot(other);
            bot.DealDamage(bot.gameObject,damage);
            LevelManger.Ins.player.HealHp((damage * LevelManger.Ins.player.lifeSteal / (float)100));
            ParticlePool.Play(ParticleType.Hit_1, TF.position, Quaternion.identity);
        }
    }
}
