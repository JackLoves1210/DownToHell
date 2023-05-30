using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUp : UICanvas
{
    public Player player;
    public Button button_1;
    public Button button_2;
    public Button button_3;

    public int index;
    public GameObject[] panelAttribute;

    public List<GameObject> gameObjects;

    public void Awake()
    {
        player = FindObjectOfType<Player>();
    }
    public override void Open()
    {
        base.Open();
        button_1.GetComponentInChildren<Text>().text = StatIndex().ToString();
        SpawnPanel(int.Parse(button_1.GetComponentInChildren<Text>().text), button_1);
        button_2.GetComponentInChildren<Text>().text = StatIndex().ToString();
        SpawnPanel(int.Parse(button_2.GetComponentInChildren<Text>().text), button_2);
        button_3.GetComponentInChildren<Text>().text = StatIndex().ToString();
        SpawnPanel(int.Parse(button_3.GetComponentInChildren<Text>().text), button_3);
    }

    public void SkipButton()
    {
        UIManager.Ins.OpenUI<GamePlay>();
        Time.timeScale = 1;
        DeSpawnGameobject(gameObjects);
        Close(0);
    }

    public int StatIndex()
    {
        int num = Random.Range(0, AttributeManager.Ins.statIndex.Count);

        return AttributeManager.Ins.statIndex[num];
    }

    public int RandomNumber()
    {
        int num = Random.Range(0, 11);
        if (IsMaxPowerUp(player))
        {
            return 11;
        }
        else
        {
            while (num < 7)
            {
                for (int i = 0; i < AttributeManager.Ins.weapons.Length; i++)
                {
                    if (num == i)
                    {
                        if (AttributeManager.Ins.weapons[i].powerUps > 5)
                        {
                            num = Random.Range(0, 11);
                        }
                        else
                        {
                            return num;
                        }
                    }
                }
            }
            while (num > 6)
            {
                if (num - 7 > player.passives.Count - 1)
                {
                    return num;
                }
                for (int i = 0; i < player.passives.Count; i++)
                {
                    if (num - 7 == i)
                    {
                        if (player.passives[i].index > 5)
                        {
                            num = Random.Range(0, 11);
                        }
                        else
                        {
                            return num;
                        }
                    }

                }
            }
        }
        return num;
    }

    public void SpawnPanel(int index, Button button)
    {
        for (int i = 0; i < panelAttribute.Length; i++)
        {
            if (index == i)
            {
                GameObject panelInstance = Instantiate(panelAttribute[i]);
                panelInstance.transform.SetParent(button.transform, false);
                gameObjects.Add(panelInstance);
            }
        }
    }

    public void DeSpawnGameobject(List<GameObject> gameObjects)
    {
        for (int i = 0; i < gameObjects.Count; i++)
        {
            Destroy(gameObjects[i]);
        }
    }
    public string IdentifyPassive(int index)
    {
        if (index > 6)
        {
            return (index - 6).ToString();
        }
        else
        {
            return index.ToString();
        }
    }

    private bool IsMaxPowerUp(Player player)
    {
        if (player.weaponDefault.powerUps > 5 && IsWeaponFull(player) && IsPassiveFull(player))
        {
            return true;
        }
        return false;
    }
    private bool IsWeaponFull(Player player)
    {
        for (int i = 0; i < player.weaponBonous.Count; i++)
        {
            if (player.weaponBonous[i].powerUps <= 5)
            {
                return false;
            }
        }
        return true;
    }
    private bool IsPassiveFull(Player player)
    {
        for (int i = 0; i < player.passives.Count; i++)
        {
            if (player.passives[i].index <= 5)
            {
                return false;
            }
        }
        return true;
    }
}