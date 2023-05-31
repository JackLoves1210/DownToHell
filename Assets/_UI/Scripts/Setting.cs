using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : UICanvas
{

    public void ContinueButton()
    {
        Close(0);
        Time.timeScale = 1;
    }
    [SerializeField] private GameObject btnTurnOffSound;
    [SerializeField] private GameObject btnTurnOnSound;
    [SerializeField] private GameObject btnTurnOffVibration;
    [SerializeField] private GameObject btnTurnOnVibration;

    public override void Open()
    {
        base.Open();
        Time.timeScale = 0;

    }
    public void ButtonHome()
    {
        UIManager.Ins.OpenUI<MainMenu>();
        LevelManager.Ins.LoseGame();
        UIManager.Ins.OpenUI<GamePlay>().CloseDirectly();
        Close(0);
    }

    public void MuteOnSound()
    {
        AudioManager.Ins.MuteHandler(false);
        btnTurnOffSound.SetActive(false);
        btnTurnOnSound.SetActive(true);
    }

    public void MuteOffSound()
    {
        AudioManager.Ins.MuteHandler(true);
        btnTurnOffSound.SetActive(true);
        btnTurnOnSound.SetActive(false);
    }
    public void MuteOnVibration()
    {
        AudioManager.Ins.MuteHandleVibrater(true);
        btnTurnOffVibration.SetActive(true);
        btnTurnOnVibration.SetActive(false);
    }

    public void MuteOffVibration()
    {
        AudioManager.Ins.MuteHandleVibrater(false);
        btnTurnOffVibration.SetActive(false);
        btnTurnOnVibration.SetActive(true);

    }
}
