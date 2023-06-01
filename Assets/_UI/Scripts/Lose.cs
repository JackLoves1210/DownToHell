using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lose : UICanvas
{
    public Text score;

    public override void Open()
    {
        base.Open();
       
    }
    public void MainMenuButton()
    {
        UIManager.Ins.OpenUI<MainMenu>();
        LevelManager.Ins.LoseGame();
        UIManager.Ins.OpenUI<GamePlay>().CloseDirectly();
        Close(0);
    }

    public void PlayAgain()
    {
        UIManager.Ins.OpenUI<GamePlay>();
        UIManager.Ins.OpenUI<GamePlay>().ActiveJoystick(true);
        LevelManager.Ins.LoseGame();
        Close(0);
    }
}
