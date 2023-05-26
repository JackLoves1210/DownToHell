using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBot : Bullet
{

    public override void OnInit(Character character, Vector3 target)
    {
        base.OnInit(character, target);
        this.character = character;
        TF.forward = (target - TF.position).normalized;
        counterTime.Start(OnDespawn, timeAlive);
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
            OnDespawn();
        }
    }
}
