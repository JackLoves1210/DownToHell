using System.Collections;
using System.Collections.Generic;
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
    
    public Weapon[] weapons;

    public bool isCanMove;
    public bool isCanAtt;
    public int level;
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
       // timeCoolDown += Time.deltaTime;
        if (targets.Count > 0)
        {
            isCanAtt = true;
            TF.LookAt(targetPoint + (TF.position.y - targetPoint.y) * Vector3.up);
        }

        //if (timeCoolDown > timeReload && isCanAtt)
        //{
        //    OnAttack();
        //    timeCoolDown = 0;
        //}

        OnAttack();

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            //i++;
            //if (i > weapons.Length-1)
            //{
            //    i = 0;
            //}
            //weaponDefault = weapons[i];
            //timeReload = weaponDefault.timeBettwenShoot;
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
        hp = maxHp;
        if (weaponDefault == null)
        {
            weaponDefault = AttributeManager.Ins.weapons[i];
            weaponDefault.OnRest();
        }
        EnableATTWeapon();
        isCanMove = true;
        movementCurve.AddKey(0.5f, moveSpeed);
        //timeReload = weaponDefault.timeBettwenShoot;
        //timeCoolDown = 10;
        healthBar.UpDateHealthBar(maxHp, hp);
        
    }
    public override void OnHit()
    {
        
        if (weaponDefault != null && weaponDefault.isCanAttack)
        {
            weaponDefault.damageWeapon = baseDame;
            weaponDefault.Shooting(this, targetPoint);
        }

        for (int i = 0; i < weaponBonous.Count; i++)
        {
            if (weaponBonous[i] != null && weaponBonous[i].isCanAttack)
            {
                weaponBonous[i].damageWeapon = baseDame;
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
            weaponDefault.timeBettwenShoot -= weaponDefault.timeBettwenShoot * 0.1f;
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
                transform.rotation = Quaternion.LookRotation(direction);
            }
        }
    }
    public void EquidWeapon(Weapon weapon) 
    {
        weapon.isCanAttack = true;
        weaponBonous.Add(weapon);
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
    public void TakeExp(int amount)
    {
        exp += amount;
        if (exp >= 1000)
        {
            LevelUp((int)exp / 1000);
            exp %=1000; 
        }
    }
    public void LevelUp(int amount)
    {
         level+= amount;
         HealHp(amount * 10);
         Debug.Log("level up");
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
            DealDamage(this.gameObject);
            healthBar.UpDateHealthBar(maxHp, hp);
        }
    }

}
