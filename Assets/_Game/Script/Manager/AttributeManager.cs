using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AttributeManager : Singleton<AttributeManager>
{
    public Weapon[] weapons;
    public List<int> currentIndexWeapons;
    public Passive[] passives;
    public List<int> currentIndexPassives;
    public List<int> statIndex;

    public int numberWeapon;
    public int numberPassives;

    public List<int> indexDeleted;
    public void Awake()
    {
        StatisticalIndex();
        currentIndexWeapons.Add(0);
    }

    public int CheckIndexWeapon(Weapon weapon)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapon == weapons[i])
            {
                return i;
            }
        }
        return 0;
    }

    public int CheckIndexPassive(Passive passive)
    {
        for (int i = 0; i < passives.Length; i++)
        {
            if (passive == passives[i])
            {
                Debug.Log(i);
                return i;
            }
        }
        return 7;
    }
    public void AttributeConfigIndex(int currentFloor)
    {
        if (currentFloor < 5)
        {
            numberWeapon = 2;
            numberPassives = 2;
        }
        if (currentFloor > 4 && currentFloor < 10)
        {
            numberWeapon = 3;
            numberPassives = 3;
        }
        if (currentFloor > 9 && currentFloor < 15)
        {
            numberWeapon = 4;
            numberPassives = 4;
        }
        if (currentFloor > 14 && currentFloor < 20)
        {
            numberWeapon = 5;
            numberPassives = 5;
        }
        if (currentFloor > 19)
        {
            numberWeapon = 6;
            numberPassives = 6;
        }
    }
    public void StatisticalIndex()
    {
        for (int i = 0; i < weapons.Length + passives.Length; i++)
        {
            for (int j = 0; j < 7; j ++)
            {
                statIndex.Add(i);
            }
        }
    }

    public void DeleteOtherIndexWeapon()
    {
        if (LevelManger.Ins.player.currentNumberWeapon == numberWeapon)
        {
            for (int i = 0; i < LevelManger.Ins.player.weaponBonous.Count; i++)
            {
                currentIndexWeapons.Add(CheckIndexWeapon(LevelManger.Ins.player.weaponBonous[i]));
            }

            var remainingIndexes = weapons.Select((weapon, index) => index).Except(currentIndexWeapons);

            foreach (int index in remainingIndexes)
            {
                indexDeleted.Add(index);
                statIndex.RemoveAll(x => x == index);
            }
        }
    }
    public void DeleteOtherIndexPassive()
    {
        if (LevelManger.Ins.player.currentNumberPassive == numberPassives)
        {
            for (int i = 0; i < LevelManger.Ins.player.passives.Count; i++)
            {
                currentIndexPassives.Add(CheckIndexPassive(LevelManger.Ins.player.passives[i]));
            }

            var remainingIndexes = passives.Select((passive, index) => index).Except(currentIndexPassives);

            foreach (int index in remainingIndexes)
            {
                indexDeleted.Add(index + 7);
                statIndex.RemoveAll(x => x == index+7);
            }
        }
    }

    public void AddMoreStat()
    {
        for (int i = 0; i < indexDeleted.Count; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                statIndex.Add(indexDeleted[i]);
            } 
        }
    }
}
