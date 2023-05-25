using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootPresent : MonoBehaviour
{
    public Passive[] passives;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAG_PLAYER))
        {
            //Player player = Cache.GetPlayer(other);
            //Weapon weapon = SelectWeapon(player);
            //while (weapon == player.weaponDefault || IsWeaponEquied(player, weapon))
            //{
            //    weapon = SelectWeapon(player);
            //}
            //player.EquidWeapon(weapon);
            HandleEventLevelUp();
            Destroy(gameObject);

            //int rand = Random.Range(0, passives.Length);
            //if (!IsLevelingUp(rand, player))
            //{
            //    AddPassive(rand, player);
            //}
            
        }
    }

    public void HandleEventLevelUp()
    {
        UIManager.Ins.OpenUI<GamePlay>().Close(0);
        Time.timeScale = 0;
        UIManager.Ins.OpenUI<LevelUp>();
        
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
    public void AddPassive(int index , Player player)
    {
        player.passives.Add(passives[index]);
        player.AddPassive(index);
    }

    private bool IsWeaponEquied(Player player , Weapon weapon)
    {
        for (int i = 0; i < player.weaponBonous.Count; i++)
        {
            if (weapon == player.weaponBonous[i])
            {
                return true;
            }
        }
        return false;
    }
    private Weapon SelectWeapon(Player player)
    {
        int randomIndex = Random.Range(0, player.weapons.Length);
        return player.weapons[randomIndex];
    }
}
