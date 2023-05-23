using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState<Bot>
{
    public void OnEnter(Bot t)
    {

    }

    public void OnExecute(Bot t)
    {
        if (t.isCanATT)
        {
            t.ChangeState(new AttackState());
        }
        else
        {
            t.Moving();
        }  
    }

    public void OnExit(Bot t)
    {

    }

}
