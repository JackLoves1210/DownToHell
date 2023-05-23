using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotManager : Singleton<BotManager>
{
    public int quantityBot;

    public List<Bot> bots;

    private void Start()
    {
        for (int i = 0; i < quantityBot; i++)
        {
            SpawnBot();
        }
    }

    public void SpawnBot()
    {
        Vector3 pos = RamdomBot.Ins.GetRandomPointOnNavMesh();
        Bot bot = SimplePool.Spawn<Bot>(PoolType.Bot, pos, Quaternion.identity);
        bots.Add(bot);
    }
}
