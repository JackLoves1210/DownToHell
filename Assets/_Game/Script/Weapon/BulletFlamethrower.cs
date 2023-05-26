using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFlamethrower : Bullet
{
    public float timeSendDame = 0.5f; 
    private float timeSendDameCounter;
    Vector3 targetPoint;

    public override void OnInit(Character character, Vector3 target)
    {
        base.OnInit(character, target);
        targetPoint = target;
        this.character = character;
        TF.forward = (target - TF.position).normalized;
        counterTime.Start(OnDespawn, timeAlive);
        isRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        counterTime.Execute();
       // TF.LookAt(targetPoint + (TF.position.y - targetPoint.y) * Vector3.up);
        //TF.position = Vector3.MoveTowards(TF.position, character.TF.position + new Vector3(0,0,TF.localScale.z/2), Time.deltaTime * 5);
        
    }
    private void OnTriggerStay(Collider other)
    {
        timeSendDameCounter -= Time.deltaTime;
        if (other.CompareTag(Constant.TAG_CHARACTER) && timeSendDameCounter < 0)
        {
            timeSendDameCounter = timeSendDame;
            Bot bot = Cache.GetBot(other);
            bot.DealDamage(bot.gameObject,damage);
        }
    }
}
