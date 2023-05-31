using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lose : UICanvas
{
    public Text score;

    public void MainMenuButton()
    {
        UIManager.Ins.OpenUI<MainMenu>();
        Close(0);
    }

    public void PlayAgain()
    {
        UIManager.Ins.OpenUI<GamePlay>();
        LevelManager.Ins.LoseGame();
        Close(0);
    }
}
