using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAG_PLAYER))
        {
            Player target = Cache.GetPlayer(other);
            if (!target.IsDead)
            {
                UIManager.Ins.OpenUI<GamePlay>().CloseDirectly();
                UIManager.Ins.OpenUI<Waiting>();
            }
        }
    }
}
