using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Stat : UICanvas
{
    public GameObject statWeapon;
    public GameObject statPassive;
    public TMP_Text maxHpTxt;
    public TMP_Text baseDamageTxt;
    public TMP_Text moveSpeedTxt;
    public TMP_Text lifeStealTxt;
    // powẻr up
    public TMP_Text powerUpMachineTxt;
    public TMP_Text powerUpShotgunTxt;
    public TMP_Text powerUpSniperTxt;
    public TMP_Text powerUpFlameTxt;
    public TMP_Text powerUpAcidTxt;
    public TMP_Text powerUpElectricTxt;
    public TMP_Text powerUpAutozoneTxt;
    public TMP_Text powerUpMaxHpTxt;
    public TMP_Text powerUpMoveSpeedTxt;
    public TMP_Text powerUpBaseDamageTxt;
    public TMP_Text powerUpLifeStealTxt;
    


    List<int> indexWeapon = new List<int>();
    public override void Open()
    {
        base.Open();
        UpdateTxt();
        UpdateStatWeapon();
        UpdateStatPassive();
        Time.timeScale = 0;
        
    }
    public void ContinueButton()
    {
        Close(0);
        Time.timeScale = 1;
    }

    public void UpdateTxt()
    {
        maxHpTxt.text = LevelManager.Ins.player.maxHp.ToString();
        baseDamageTxt.text = LevelManager.Ins.player.baseDame.ToString();
        moveSpeedTxt.text = LevelManager.Ins.player.moveSpeed.ToString();
        lifeStealTxt.text = LevelManager.Ins.player.lifeSteal.ToString() + "%";
        //
        powerUpMachineTxt.text = LevelManager.Ins.player.weaponDefault.powerUps.ToString();
        powerUpShotgunTxt.text = AttributeManager.Ins.weapons[1].powerUps.ToString();
        powerUpSniperTxt.text = AttributeManager.Ins.weapons[2].powerUps.ToString();
        powerUpFlameTxt.text = AttributeManager.Ins.weapons[3].powerUps.ToString();
        powerUpAcidTxt.text = AttributeManager.Ins.weapons[4].powerUps.ToString();
        powerUpElectricTxt.text = AttributeManager.Ins.weapons[5].powerUps.ToString();
        powerUpAutozoneTxt.text = AttributeManager.Ins.weapons[6].powerUps.ToString();
        powerUpMaxHpTxt.text = LevelManager.Ins.player.passives[0].index.ToString();
        powerUpMoveSpeedTxt.text = LevelManager.Ins.player.passives[1].index.ToString();
        powerUpBaseDamageTxt.text = LevelManager.Ins.player.passives[2].index.ToString();
        powerUpLifeStealTxt.text = LevelManager.Ins.player.passives[3].index.ToString();
    }

    public void UpdateStatWeapon()
    {
        statWeapon.transform.GetChild(0).gameObject.SetActive(true);
        
        CheckStatWeapon();
        
    }

    public void CheckStatWeapon()
    {
        foreach (var value in LevelManager.Ins.player.weaponBonous)
        {
            int index = Array.IndexOf(AttributeManager.Ins.weapons, value);
            if (index != -1)
            {
                indexWeapon.Add(index);
            }
        }
        if (indexWeapon.Count > 0)
        {
            for (int i = 0; i < indexWeapon.Count; i++)
            {
                statWeapon.transform.GetChild(indexWeapon[i]).gameObject.SetActive(true);
            }
        } 
    }
    public void UpdateStatPassive()
    {
        CheckStatPassive();
    }

    public void CheckStatPassive()
    {
        for (int i = 0; i < statPassive.transform.childCount; i++)
        {
            statPassive.transform.GetChild(i).gameObject.SetActive(true);
        }
    }
}
