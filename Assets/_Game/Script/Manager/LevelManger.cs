using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManger : Singleton<LevelManger>
{
    public Player player;
    //public StateGame stateGame;
    [SerializeField] private Map[] mapFrefab;
    [SerializeField] private List<Map> maps;
    [SerializeField] private Floor[] floors;
    public int currentFloor;

    public float timeWaiting;

    private void Awake()
    {
        currentFloor = 1;
        for (int i = 0; i < mapFrefab.Length; i++)
        {
            Map map = SimplePool.Spawn<Map>(mapFrefab[i]);
            maps.Add(map);
            map.gameObject.SetActive(false);
        }
        if (maps.Count > 0)
        {
            maps[currentFloor - 1].gameObject.SetActive(true);
        }
        switch (GameManager.gameState)
        {
            case StateGame.Mainmenu:
                UIManager.Ins.OpenUI<MainMenu>();
                break;
            default:
                break;
        }
    }
    public void OnReset()
    {
        player.OnRevive();
        for (int i = 0; i < BotManager.Ins.bots.Count; i++)
        {
            BotManager.Ins.bots[i].OnDespawn();
            SimplePool.Despawn(BotManager.Ins.bots[i]);
        }
        BotManager.Ins.bots.Clear();
        // SimplePool.CollectAll();
    }

    public void SpawnBot()
    {
        BotManager.Ins.SpawnBot(PoolType.Bot_1, floors[currentFloor - 1].quantityMeleeBots, currentFloor - 1);
        BotManager.Ins.SpawnBot(PoolType.Bot_2, floors[currentFloor - 1].quantityRangedBots, currentFloor - 1);
    }

    public void LoseGame()
    {
        OnReset();
        UIManager.Ins.OpenUI<Lose>().CloseDirectly();
        Invoke(nameof(SpawnBot), timeWaiting);
        PlayerPrefs.Save();
    }

    public void NextLevelGame()
    {
        OnReset();
        NextLevel();
        Invoke(nameof(SpawnBot), timeWaiting);
        PlayerPrefs.Save();
    }


    public void NextLevel()
    {
        SimplePool.Despawn(maps[currentFloor - 1]);
        currentFloor++; 
        if (currentFloor > 19)
        {
            maps[19].gameObject.SetActive(true);
        }
        else
        {
            Debug.Log(currentFloor);
            maps[currentFloor - 1].gameObject.SetActive(true);
        }
        PlayerPrefs.Save();
    }

}
