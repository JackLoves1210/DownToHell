using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState<Bot>
{
    public void OnEnter(Bot t)
    {

    }

    public void OnExecute(Bot t)
    {
        if (t.isFollowPlayer)
        {
            t.FollowPlayer();
        }

        if (t.isMoving)
        {
            t.ChangeState(new PatrolState());
        }
    }

    public void OnExit(Bot t)
    {

    }

}
