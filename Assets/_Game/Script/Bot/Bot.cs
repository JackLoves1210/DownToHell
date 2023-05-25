using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    [Header("Bot")]
    public Player player;
    private IState<Bot> currentState;
    public NavMeshAgent agent;
    public float range;
    public Transform centrePoint;
    Vector3 nextPoint;

    public Exp[] exps;


    public bool isCanATT;
    public bool isRoaming;

    //shoot
    public bool isShootable = false;
    public GameObject bullet;
    public float bulletSpeed;
    public float timeBtwFire;
    public float fireCoolDown;

    private void Start()
    {
        OnInit();
    }

    void Update()
    {
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }

        if (hp <= 0)
        {
            OnDeath();
        }
    }

    public override void OnInit()
    {
        base.OnInit();
        hp = maxHp;
        if (weaponDefault != null)
        {
            timeBtwFire = weaponDefault.timeBettwenShoot;
        }
        isCanATT = false;
        ChangeState(new IdleState());
    }
    public void Moving()
    {
        agent.enabled = true;
        if (agent.enabled)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (RandomPoint(centrePoint.position, range, out nextPoint))
                {
                  //  ChangeAnim(Constant.ANIM_RUN);
                    Debug.DrawRay(nextPoint, Vector3.up, Color.blue, 1.0f);
                    agent.SetDestination(nextPoint);
                }
            }
            if (IsDestination(nextPoint))
            {
                ChangeState(new IdleState());
            }
        }
    }
    public void EnemyFireBullet()
    {
        var bulletTmp = Instantiate(bullet, TF.position,Quaternion.identity);
        Rigidbody rb = bulletTmp.GetComponent<Rigidbody>();
        Vector3 direction = player.TF.position - TF.position;
        rb.velocity = direction * bulletSpeed ;
        
    }

    public override void OnHit()
    {
        weaponDefault.damageWeapon = baseDame;
        weaponDefault.Shooting(this, player.TF.position);
    }


    public Vector3 FindTarget()
    {
        Vector3 playerPos = player.TF.position;
        if (isRoaming)
        {
            return playerPos + (Random.Range(5f, 10f) * new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1, 1)).normalized);
        }
        else
        {
            return playerPos;
        }
    }


    public void FollowTarget()
    {
        Vector3 target = FindTarget();
        if (target != null)
        {
            agent.SetDestination(target);
        }
    }

    bool IsDestination(Vector3 targetPoint) => Vector3.Distance(transform.position, targetPoint) - Mathf.Abs(transform.position.y - targetPoint.y) < 0.1f;
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
    public override void OnDeath()
    {
        base.OnDeath();
        FallingExp();
        SimplePool.Despawn(this);
        ReSpawn();
        LevelManger.Ins.Player.RemoveTarget(this);
    }

    public void ReSpawn()
    {
        OnInit();
        if (isRoaming)
        {
            BotManager.Ins.SpawnBotRoaming();
        }
        else
        {
            BotManager.Ins.SpawnBot();
        }
    }
    public void FallingExp()
    {
        int rand = Random.Range(0, 2);
        Exp exp = SimplePool.Spawn<Exp>(exps[rand], TF.position, Quaternion.identity);
        exp.TF.position = new Vector3(exp.TF.position.x, exp.TF.position.y - 0.8f, exp.TF.position.z);
        if (rand == 0)
        {
            exp.exp = Random.Range(50, 1000);
        }
        else
        {
            exp.exp = Random.Range(1000, 4444);
        }
    }

    public void ChangeState(IState<Bot> state)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = state;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag(Constant.TAG_PLAYER))
    //    {
    //     //   DealDamage(collision.gameObject);
    //    }
    //}
}
