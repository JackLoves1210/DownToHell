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
        for (int i = 0; i < quantityBot/2; i++)
        {
            SpawnBotRoaming();
        }
    }

    public void SpawnBot()
    {
        Vector3 pos = RamdomBot.Ins.GetRandomPointOnNavMesh();
        Bot bot = SimplePool.Spawn<Bot>(PoolType.Bot_1, pos, Quaternion.identity);
        bots.Add(bot);
    }

    public void SpawnBotRoaming()
    {
        Vector3 pos = RamdomBot.Ins.GetRandomPointOnNavMesh();
        Bot bot = SimplePool.Spawn<Bot>(PoolType.Bot_2, pos, Quaternion.identity);
        bots.Add(bot);
    }


}
