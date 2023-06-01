using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotManager : Singleton<BotManager>
{
    public int quantityMeleeBots;
    public int quantityRangedBots;
    public List<Bot> bots;

    public static float tutolDamage;
    // stats bot
    public float moveSpeed, HP, Damage;
    private void Start()
    {
        tutolDamage = 0;
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
        Vector3 pos = RandomPoint();
        //pos = new Vector3(pos.x, 0, pos.z);
        Bot bot = SimplePool.Spawn<Bot>(poolType, pos, Quaternion.identity);
        bots.Add(bot);
    }

    public void SpawnBot(PoolType poolType, int quantity , int numberOfFloor)
    {
        for (int i = 0; i < quantity; i++)
        {
            Vector3 pos = RandomPoint();
            //pos = new Vector3(pos.x, 0, pos.z);
            Bot bot = SimplePool.Spawn<Bot>(poolType, pos, Quaternion.identity);
            bot.BotPowerUpgrade(bot, moveSpeed, HP, Damage,numberOfFloor);
            //BotPowerUpgrade(bot, numberOfFloor);
            bots.Add(bot);
        }
    }


    public Vector3 RandomPoint()
    {
        Vector3 randPoint = Vector3.zero;
        float size = 12f;

        int maxAttempts = 50;
        int attemptCount = 0;

        while (attemptCount < maxAttempts)
        {
            randPoint = RamdomBot.Ins.RandomPoint();
            if (Vector3.Distance(randPoint, LevelManager.Ins.player.TF.position) >= size)
            {
                return randPoint;
            }

            attemptCount++;
        }

        Debug.LogWarning("Failed to find a valid random point");
        return Vector3.left *8f; 
    }

}
