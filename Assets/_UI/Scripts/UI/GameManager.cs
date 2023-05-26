using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
   
  
    //[SerializeField] UserData userData;
    //[SerializeField] CSVData csv;
    public static StateGame gameState;

    // Start is called before the first frame update
    protected void Awake()
    {
        //base.Awake();
        Input.multiTouchEnabled = false;
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        int maxScreenHeight = 1280;
        float ratio = (float)Screen.currentResolution.width / (float)Screen.currentResolution.height;
        if (Screen.currentResolution.height > maxScreenHeight)
        {
            Screen.SetResolution(Mathf.RoundToInt(ratio * (float)maxScreenHeight), maxScreenHeight, true);
        }

        //csv.OnInit();
        //userData?.OnInitData();

        ChangeState(StateGame.Mainmenu);

        CameraFollow.Ins.ChangeState(CameraFollow.State.Gameplay);
        //UIManager.Ins.OpenUI<GamePlay>();
    }

    public static void ChangeState(StateGame state)
    {
        gameState = state;
    }

    //public static bool IsState(GameState state)
    //{
    //    return gameState == state;
    //}

}
