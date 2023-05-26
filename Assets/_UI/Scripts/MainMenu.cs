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
        
        UIManager.Ins.OpenUI<GamePlay>();
        Close(0);
    }
}
