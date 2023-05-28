using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneATTBot : MonoBehaviour
{
    [SerializeField] private Bot bot;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAG_PLAYER))
        {
            Player player = Cache.GetPlayer(other);
            if (!player.IsDead)
            {
                bot.isCanATT = true;
                bot.player = player;
            }
            else bot.isCanATT = false; 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constant.TAG_PLAYER))
        {
            bot.isCanATT = false;
        }
    }
}
