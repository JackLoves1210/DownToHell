using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSender : MonoBehaviour
{
    public float hp;
    public int maxHp;
    public int damageSender;
    public int armor;
    public float critDamage = 1.5f;
    public float critRate = 50;

    public void TakeDamage(int damage)
    {
        int ramdomNum = Random.Range(0, 101);
        if (ramdomNum >= critRate)
        {
            hp -= critDamage*damage - (critDamage*damage * armor / 100);
        }
        else
        {
            hp -= damage - (damage * armor / 100);
        }

        if (hp < 0)
        {
            hp = 0;
        }
    }

    public void HealHp(int healHP)
    {
        hp += healHP;
        if (hp > maxHp)
        {
            hp = maxHp;
        }
    }

    public void DealDamage(GameObject target)
    {
        var atm = target.GetComponent<DamageSender>();
        if (atm != null)
        {
            atm.TakeDamage(damageSender);
        }
    }
    
}
