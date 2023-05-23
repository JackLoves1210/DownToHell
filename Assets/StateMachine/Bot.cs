using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    [Header("Bot")]
    private IState<Bot> currentState;
    public NavMeshAgent agent;
    public float range;
    public Transform centrePoint;
    Vector3 nextPoint;

    public Exp[] exps;
    public bool isCanATT;
    public Player target;

    private void Start()
    {
        ChangeState(new IdleState());
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
                    Debug.DrawRay(nextPoint, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
                    agent.SetDestination(nextPoint);
                }
            }
            if (IsDestination())
            {
                ChangeState(new IdleState());
            }
        }
    }
    bool IsDestination() => Vector3.Distance(transform.position, nextPoint) - Mathf.Abs(transform.position.y - nextPoint.y) < 0.1f;
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
        BotManager.Ins.SpawnBot();
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

    public void FollowPlayer()
    {
        if (target != null)
        {
            agent.SetDestination(target.TF.position);
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
