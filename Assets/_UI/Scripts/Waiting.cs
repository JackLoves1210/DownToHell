using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waiting : UICanvas
{
    public override void Open()
    {
        base.Open();
        LevelManager.Ins.NextLevelGame();
        Invoke(nameof(WaitTiming), LevelManager.Ins.timeWaiting);
    }
    public void WaitTiming()
    {
        UIManager.Ins.OpenUI<GamePlay>();
        this.CloseDirectly();
    }
}
