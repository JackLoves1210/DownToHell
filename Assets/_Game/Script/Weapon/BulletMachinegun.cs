using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMachinegun : Bullet
{
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
        }
        //  ParticlePool.Play(ParticleType.Hit, transform.position, Quaternion.identity);

    }
}
