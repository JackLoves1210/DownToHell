using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : GameUnit
{
    public float moveSpeed = 10f;
    public float TIME_ALIVE = 1.2f;
    public int damage;
    public CounterTime counterTime = new CounterTime();
    public bool isRunning;
    public Character character;

    public virtual void OnInit(Character character, Vector3 target)
    {

    }

    public void OnDespawn()
    {
        SimplePool.Despawn(this);
    }
}
