using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePlay : UICanvas
{
    public TMP_Text numberFloorText;

    public int mumberFloor;
    public override void Open()
    {
        base.Open();
        mumberFloor = LevelManger.Ins.currentFloor;
        NumberFloor();
    }
    public void NumberFloor()
    {
        numberFloorText.text = mumberFloor.ToString();
    }
    public void SettingButton()
    {
        UIManager.Ins.OpenUI<Setting>();
    }
}
