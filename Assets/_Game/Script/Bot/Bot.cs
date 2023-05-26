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
    public bool isMoving;

    //shoot
    public bool isShootable = false;
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
        OnDespawn();
        ReSpawn();
        LevelManger.Ins.player.RemoveTarget(this);
    }

    public void ReSpawn()
    {
        OnInit();
        if (isRoaming)
        {
            BotManager.Ins.SpawnBot(PoolType.Bot_2);
        }
        else
        {
            BotManager.Ins.SpawnBot(PoolType.Bot_1);
        }
    }

    public void OnDespawn()
    {
        this.IsDead = false;
        SimplePool.Despawn(this);
        //BotManager.Ins.bots.Remove(this);
    }
    public void FallingExp()
    {
        int rand = Random.Range(0, 20);
        if (rand == 0)
        {
            Exp exp = SimplePool.Spawn<Exp>(exps[1], TF.position, Quaternion.identity);
            exp.TF.position = new Vector3(exp.TF.position.x, exp.TF.position.y - 0.8f, exp.TF.position.z);
            exp.exp = Random.Range(999, 4444);
        }
        else
        {
            Exp exp = SimplePool.Spawn<Exp>(exps[0], TF.position, Quaternion.identity);
            exp.TF.position = new Vector3(exp.TF.position.x, exp.TF.position.y - 0.8f, exp.TF.position.z);
            exp.exp = Random.Range(50, 150);

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

    public void StopMoving()
    {
        agent.enabled = false;
    }
    public void ActiveMoving()
    {
        agent.enabled = true;
        isMoving = true;
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag(Constant.TAG_PLAYER))
    //    {
    //     //   DealDamage(collision.gameObject);
    //    }
    //}
}
