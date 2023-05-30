using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClickAttribute : MonoBehaviour
{
    public LevelUp levelUp;
    public Button myButton;
   
    public void GamePlayButton(int index)
    {
        string buttonText = myButton.GetComponentInChildren<Text>().text;
        index = int.Parse(buttonText);
        AttributeManager.Ins.statIndex.Remove(index);
        if (index != 11)
        {
            if (index > 6)
            {
                if (!IsLevelingUp(index - 7, levelUp.player))
                {
                    AddPassive(index - 7, levelUp.player);
                }
            }
            else
            {
                if (!levelUp.player.IsWeaponUpgrade(AttributeManager.Ins.weapons[index]))
                {
                    levelUp.player.EquidWeapon(AttributeManager.Ins.weapons[index]);
                }
            }
        }
        
        UIManager.Ins.OpenUI<GamePlay>();
        levelUp.DeSpawnGameobject(levelUp.gameObjects);
        Time.timeScale = 1;
        levelUp.Close(0);
    }

    

    public bool IsLevelingUp(int index, Player player)
    {
        for (int i = 0; i < player.passives.Count; i++)
        {
            if (index == (int)player.passives[i].passive)
            {
                player.passives[i].index += 1;
                player.passives[i].statGrowth = 5 * player.passives[i].index;
                player.AddPassive(index);
                return true;
            }
        }
        return false;

    }
    public void AddPassive(int index, Player player)
    {
        player.passives.Add(AttributeManager.Ins.passives[index]);
        player.AddPassive(index);
        player.currentNumberPassive++;
        if (player.currentNumberPassive >= 2)
        {
            AttributeManager.Ins.DeleteOtherIndexPassive();
        }
        
    }
}
