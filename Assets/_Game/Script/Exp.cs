using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp : GameUnit
{
    public int exp;

    Coroutine moveCoroutine;
    public void OnDespawn()
    {
        SimplePool.Despawn(this);
    }

    public void MoveToTaget(Vector3 target)
    {
        if (moveCoroutine != null) StopCoroutine(moveCoroutine);

        moveCoroutine = StartCoroutine(MoveToTagetCoroutine(target));
        
    }

    IEnumerator MoveToTagetCoroutine(Vector3 target)
    {
        float distance = Vector3.Distance(TF.position, target);
        while (distance > 0.01f)
        {
            TF.position = Vector3.MoveTowards(TF.position, target, Time.deltaTime*12f);
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAG_PLAYER))
        {
            Player player = Cache.GetPlayer(other);
            player.TakeExp(exp,player.realExp);
            player.healthBar.UpDateHealthBar(player.maxHp, player.hp);
            //Destroy(this.gameObject);
            OnDespawn();
        }
    }
}
