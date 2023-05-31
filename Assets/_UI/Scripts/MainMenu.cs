using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : UICanvas
{
    public void PlayButton()
    {
        GameManager.ChangeState(StateGame.GamePlay);
        for (int i = 0; i < BotManager.Ins.bots.Count; i++)
        {
            BotManager.Ins.bots[i].ActiveMoving();
        }
        Time.timeScale = 1;
        LevelManager.Ins.player.isCanAtt = true;
        UIManager.Ins.OpenUI<GamePlay>().UpDateExpBar(LevelManager.Ins.player.realExp, LevelManager.Ins.player.exp);
        Close(0);
    }
}
