using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : Character
{
    [Header("Player")]

    public List<Weapon> weaponBonous;

    [SerializeField] protected Character target;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private Rigidbody rb;
  //  /[SerializeField] private float timeReload;
    [SerializeField] public HealthBar healthBar;

    public AnimationCurve movementCurve;


    //public float timeCoolDown = 0.4f;
    public List<Character> targets;
    public List<Passive> passives;

    public int currentNumberWeapon;
    public int currentNumberPassive;

    public bool isCanMove;
    public bool isCanAtt;
    public int level;
    public int maxExp;
    public int realExp;
    public float rateGrownExp;
    public int exp;
    
    Vector3 targetPoint;
    private CounterTime counter = new CounterTime();
    public CounterTime Counter => counter;

    public float timeReceiveDame = 0.2f;
    private float timeReceiveDameCounter;

    int i = 0;
    private float time;

    void Start()
    {
        OnInit();
    }
    void Update()
    {
        if (!IsDead)
        {
            if (targets.Count > 0)
            {
                isCanAtt = true;
                TF.LookAt(targetPoint + (TF.position.y - targetPoint.y) * Vector3.up);
            }
            OnAttack();
            if (hp <= 0)
            {
                OnDeath();
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            //i++;
            //if (i > weapons.Length-1)
            //{
            //    i = 0;
            //}
            //weaponDefault = weapons[i];
        }
    }
    private void FixedUpdate()
    {
        Move();
    }
    public override void OnInit()
    {
        base.OnInit();
        //OnEnablePassive();
        realExp = maxExp;
        hp = maxHp;
        if (weaponDefault == null)
        {
            weaponDefault = AttributeManager.Ins.weapons[i];
           // weaponDefault.OnRest();
        }   
        for (int i = 0; i < AttributeManager.Ins.weapons.Length; i++)
        {
            AttributeManager.Ins.weapons[i].OnRest();
        }
        EnableATTWeapon();
        isCanMove = true;
        movementCurve.AddKey(0.5f, moveSpeed);
        healthBar.UpDateHealthBar(maxHp, hp);
    }
    public override void OnHit()
    {
        
        if (weaponDefault != null && weaponDefault.isCanAttack)
        {
            weaponDefault.Shooting(this, targetPoint);
        }

        for (int i = 0; i < weaponBonous.Count; i++)
        {
            if (weaponBonous[i] != null && weaponBonous[i].isCanAttack)
            {
                weaponBonous[i].Shooting(this, targetPoint);
            }
        }
    }
    public void OnEnablePassive()
    {
        for (int i = 0; i < passives.Count; i++)
        {
            if (passive.MaxHP == passives[i].passive)
            {
                maxHp += maxHp * (passives[i].statGrowth* passives[i].index / 100);
            }

            if (passive.MoveSpeed == passives[i].passive)
            {
                moveSpeed += moveSpeed * (passives[i].statGrowth * passives[i].index / 100);
            }

            if (passive.Might == passives[i].passive)
            {
                baseDame += baseDame * (passives[i].statGrowth * passives[i].index / 100);
            }

            if (passive.Lifesteal == passives[i].passive)
            {
                lifeSteal += lifeSteal * (passives[i].statGrowth * passives[i].index / 100);
            }

        }
    }
    
    public bool IsWeaponUpgrade(Weapon weapon)
    {
        if (weapon == weaponDefault)
        {
            weaponDefault.timeBettwenShoot -= weaponDefault.timeBettwenShoot * 0.1f; // tăng số lượng đạn
            weaponDefault.damageWeapon += weaponDefault.damageWeapon * 0.2f;        // tăng dame vk
            weaponDefault.timeBulletAlive += weaponDefault.timeBulletAlive * 0.2f;   // tăng tầm đánh vk
            weaponDefault.powerUps++;
            return true;
        }
        else
        {
            for (int i = 0; i < weaponBonous.Count; i++)
            {
                if (weapon == weaponBonous[i])
                {
                    weaponBonous[i].timeBettwenShoot -= weaponBonous[i].timeBettwenShoot * 0.1f;  
                    weaponBonous[i].damageWeapon += weaponBonous[i].damageWeapon * 0.2f;
                    weaponBonous[i].timeBulletAlive += weaponBonous[i].timeBulletAlive * 0.2f;
                    weaponBonous[i].powerUps++;
                    return true;
                }
            }
        }
        return false;
    }

    public void AddPassive(int index)
    {
        if (index == (int)passive.MaxHP) 
        {
            maxHp += maxHp * (Constant.STAT_GROWTH / (float)100);
        }
        if (index == (int)passive.MoveSpeed) 
        {
            moveSpeed += moveSpeed * (Constant.STAT_GROWTH * 2/ (float)100);
        }
        if (index == (int)passive.Might)
        {
            baseDame += baseDame * (Constant.STAT_GROWTH / (float)100);
        }    
        if (index == (int)passive.Lifesteal)
        {
            lifeSteal += Constant.STAT_GROWTH;
        }
    }

    public Character GetTargetInRange()
    {
        Character target = null;
        float distance = float.PositiveInfinity;
        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i] != null && !targets[i].IsDead && targets[i] != this)
            {
                float dis = Vector3.Distance(TF.position, targets[i].TF.position);
                if (dis < distance)
                {
                    distance = dis;
                    target = targets[i];
                }
            }
        }

        return target;
    }
    private void Move()
    {
        moveSpeed = movementCurve.Evaluate(time);
        time += Time.deltaTime;
        if (isCanMove)
        {
            if (Input.GetMouseButton(0) && JoystickControl.direct != Vector3.zero)
            {
             //   _isMove = true;
                rb.MovePosition(rb.position + JoystickControl.direct * moveSpeed * Time.fixedDeltaTime);
              //  ChangeAnim(Constant.ANIM_RUN);
                Vector3 direction = Vector3.RotateTowards(transform.forward, JoystickControl.direct, rotateSpeed * Time.deltaTime, 0.0f);
                TF.rotation = Quaternion.LookRotation(direction);
               
            }
        }
    }
    public void EquidWeapon(Weapon weapon) 
    {
        weapon.isCanAttack = true;
        weaponBonous.Add(weapon);
        currentNumberWeapon++;
        AttributeManager.Ins.DeleteOtherIndexWeapon();
    }
    private void EnableATTWeapon()
    {
        weaponDefault.isCanAttack = true;
        for (int i = 0; i < weaponBonous.Count; i++)
        {
            weaponBonous[i].isCanAttack = true;
        }
    }
    public override void OnAttack()
    {
        base.OnAttack();
        target = GetTargetInRange();
        if (target != null && !target.IsDead )//&& weaponDefault.isCanAttack)
        {
            targetPoint = target.TF.position;
            
            //ChangeAnim(Constant.ANIM_ATTACK);
            OnHit();
        }
 
    }
    public override void OnDeath()
    {
        base.OnDeath();
        IsDead = true;
        AudioManager.Ins.Play(Constant.SOUND_LOST);
        UIManager.Ins.OpenUI<GamePlay>().CloseDirectly();
        UIManager.Ins.OpenUI<Lose>();
    }
    public void OnRevive()
    {
        healthBar.UpDateHealthBar(maxHp, hp);                                
        ResetPosition();
        IsDead = false;
        target = null;
        targets.Clear();
    }
    private void ResetPosition()
    {
        TF.position = new Vector3(0,0.5f,0);
        TF.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
    }
    public void TakeExp(int amount, int expToLevelUp)
    {
        exp += amount;
        if (exp >= expToLevelUp)
        {
            LevelUp((int)exp / expToLevelUp);
            exp %=1000; 
        }
        UIManager.Ins.OpenUI<GamePlay>().UpDateExpBar(realExp,exp);
    }
    public void HandleExpBonus()
    {
        float expBonus = maxExp + maxExp * rateGrownExp * level;
        realExp = (int)expBonus;
    }
    public void LevelUp(int amount)
    {
         level+= amount;
         ParticlePool.Play(ParticleType.LevelUp_1, TF.position,Quaternion.identity);
         HealHp(amount * 10);
         HandleExpBonus();
         UIManager.Ins.OpenUI<GamePlay>().NumberLevel();
         HandleEventLevelUp();
    }
    public void HandleEventLevelUp()
    {
        UIManager.Ins.OpenUI<GamePlay>().Close(0);
        Time.timeScale = 0;
        AudioManager.Ins.Play(Constant.SOUND_LEVELUP);
        UIManager.Ins.OpenUI<LevelUp>();

    }
    public void RemoveTarget(Character character)
    {
        if (!character.IsDead)
        {
            targets.Remove(character);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        timeReceiveDameCounter -= Time.deltaTime;
        if (collision.gameObject.CompareTag(Constant.TAG_CHARACTER) && timeReceiveDameCounter < 0)
        {
            timeReceiveDameCounter = timeReceiveDame;
            DealDamage(this.gameObject, collision.gameObject.GetComponent<Character>().baseDame);
            healthBar.UpDateHealthBar(maxHp, hp);
            AudioManager.Ins.Play(Constant.SOUND_DIE);
        }
    }

}
