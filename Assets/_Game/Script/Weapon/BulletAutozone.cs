using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAutozone : Bullet
{
    public float timeSendDame = 0.5f;
    private float timeSendDameCounter;

    public override void OnInit(Character character, Vector3 target)
    {
        base.OnInit(character, target);
        this.character = character;
        counterTime.Start(OnDespawn, timeAlive);
        isRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        counterTime.Execute();
        TF.position = Vector3.Lerp(TF.position, character.TF.position, Time.deltaTime * 10);
        TF.rotation = Quaternion.identity;
    }
    private void OnTriggerStay(Collider other)
    {
        timeSendDameCounter -= Time.deltaTime;
        if (other.CompareTag(Constant.TAG_CHARACTER) && timeSendDameCounter < 0)
        {
            timeSendDameCounter = timeSendDame;
            Bot bot = Cache.GetBot(other);
            bot.DealDamage(bot.gameObject, damage);
            LevelManager.Ins.player.HealHp((damage * LevelManager.Ins.player.lifeSteal / (float)100));
            ParticlePool.Play(ParticleType.PlasmaExplosionYellow, bot.TF.position, Quaternion.identity);
            AudioManager.Ins.Play(Constant.SOUND_ATOMIC);
        }
    }
}
