using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waiting : UICanvas
{
    public override void Open()
    {
        base.Open();
        LevelManger.Ins.NextLevelGame();
        Invoke(nameof(WaitTiming), LevelManger.Ins.timeWaiting);
    }
    public void WaitTiming()
    {
        UIManager.Ins.OpenUI<GamePlay>();
        this.CloseDirectly();
    }
}
