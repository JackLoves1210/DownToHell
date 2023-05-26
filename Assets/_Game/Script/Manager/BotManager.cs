using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotManager : Singleton<BotManager>
{
    public int quantityMeleeBots;
    public int quantityRangedBots;
    public List<Bot> bots;

    // stats bot
    public float moveSpeed, HP, Damage;
    private void Start()
    {
        for (int i = 0; i < quantityMeleeBots; i++)
        {
            SpawnBot(PoolType.Bot_1);
        }
        for (int i = 0; i < quantityRangedBots; i++)
        {
            SpawnBot(PoolType.Bot_2);
        }
    }

    public void SpawnBot(PoolType poolType)
    {
        Vector3 pos = RamdomBot.Ins.GetRandomPointOnNavMesh();
        Bot bot = SimplePool.Spawn<Bot>(poolType, pos, Quaternion.identity);
        bots.Add(bot);
    }

    public void SpawnBot(PoolType poolType, int quantity , int numberOfFloor)
    {
        for (int i = 0; i < quantity; i++)
        {
            Vector3 pos = RamdomBot.Ins.GetRandomPointOnNavMesh();
            Bot bot = SimplePool.Spawn<Bot>(poolType, pos, Quaternion.identity);
            BotPowerUpgrade(bot, moveSpeed, HP, Damage,numberOfFloor);
            bots.Add(bot);
        }
    }

    public void BotPowerUpgrade(Bot bot,float moveSpeed,float HP,float Damage, int index)
    {
        bot.agent.speed += bot.agent.speed * moveSpeed *index;
        bot.maxHp += bot.maxHp * HP * index;
        bot.baseDame += bot.baseDame * Damage * index;
    }

}
