using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePlay : UICanvas
{
    public TMP_Text numberFloorText;
    public TMP_Text numberLevelText;
    public ExpBar expBar;
    public int mumberFloor;

    public override void Open()
    {
        base.Open();
        mumberFloor = LevelManager.Ins.currentFloor;
        NumberFloor();
        NumberLevel(); 
    }
    public void NumberFloor()
    {
        numberFloorText.text = mumberFloor.ToString();
    }
    public void NumberLevel()
    {
        numberLevelText.text = LevelManager.Ins.player.level.ToString();
    }

    public void UpDateExpBar(float realExp, float exp)
    {
        expBar.UpDateExpBar(realExp, exp);
    }
    public void SettingButton()
    {
        UIManager.Ins.OpenUI<Setting>();
    }

    public  void StatsButton()
    {
        UIManager.Ins.OpenUI<Stat>();
    }
}
