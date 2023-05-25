using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay : UICanvas
{
    public void WinButton()
    {
        
        Close(0);
    }

    public void LoseButton()
    {
        UIManager.Ins.OpenUI<Lose>().score.text = Random.Range(0, 100).ToString(); 
        Close(0);
    }

    public void SettingButton()
    {
        UIManager.Ins.OpenUI<Setting>();
    }
}
