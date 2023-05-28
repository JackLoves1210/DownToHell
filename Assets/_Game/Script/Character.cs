using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : GameUnit
{
    [Header("Character")]

    [SerializeField] Animator animator;

    public float baseDame;
    public float moveSpeed;
    public float armor;
    public float critRate;
    public float critDamage;
    public float maxHp;
    public float hp;
    public float lifeSteal;

    public Weapon weaponDefault;

    public bool IsDead;//{ get; protected set; }
    private string currentAnim;
    public virtual void OnInit() 
    {
        
    }

    public void TakeDamage(float damage)
    {
        int ramdomNum = Random.Range(0, 101);
        if (ramdomNum <= critRate)
        {
            hp -= critDamage * damage - (critDamage * damage * armor / 100);
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
    public void HealHp(float healHP)
    {
        hp += healHP;
        if (hp > maxHp)
        {
            hp = maxHp;
        }
    }
    public void DealDamage(GameObject target,float dameSender)
    {
        var atm = target.GetComponent<Character>();
        if (atm != null)
        {
            atm.TakeDamage(dameSender);
        }
    }
    public virtual void OnHit() { }
    public virtual void OnDeath() { }
    public virtual void OnAttack() 
    {
      
    }
    public void ChangeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            animator.ResetTrigger(animName);
            currentAnim = animName;
            animator.SetTrigger(currentAnim);
        }
    }
}
