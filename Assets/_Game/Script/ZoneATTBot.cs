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
            bot.isCanATT = true;
            Player player = Cache.GetPlayer(other);
            bot.target = player;
        }
    }
}
