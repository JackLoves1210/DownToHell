using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelArsenal;
public class Bullet : GameUnit
{
    public float moveSpeed = 10f;
    public float timeAlive;
    public float damage;
    public CounterTime counterTime = new CounterTime();
    public bool isRunning;
    public Character character;
    public Vector3 defaultScaleSize ;
    public BulletMissile projectileParticle;
    public virtual void OnInit(Character character, Vector3 target)
    {
        defaultScaleSize = TF.localScale;
    }

    public void OnDespawn()
    {
        SimplePool.Despawn(this);
    }

    public void DespawnSFX()
    {
        projectileParticle.TF.parent = SimplePool.Root;
        SimplePool.Despawn(projectileParticle);
    }

    public void OnDespawnAll()
    {
        OnDespawn();
        DespawnSFX();
    }

}
