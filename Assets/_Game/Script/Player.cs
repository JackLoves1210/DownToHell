using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [Header("Player")]

    [SerializeField] protected Character target;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float timeReload;
    [SerializeField] public HealthBar healthBar;


   public float timeCoolDown = 0.4f;

    public List<Character> targets;

    public Weapon[] weapons;
    public Weapon weapon;


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

    void Start()
    {
        OnInit();
    }
    void Update()
    {
        timeCoolDown += Time.deltaTime;
        if (targets.Count > 0)
        {
            isCanAtt = true;
            TF.LookAt(targetPoint + (TF.position.y - targetPoint.y) * Vector3.up);
        }

        if (timeCoolDown > timeReload && isCanAtt)
        {
            OnAttack();
            timeCoolDown = 0;
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            i++;
            if (i > weapons.Length-1)
            {
                i = 0;
            }
            weapon = weapons[i];
            timeReload = weapon.timeBettwenShoot;
        }
        
    }
    private void FixedUpdate()
    {
        Move();
    }

    public override void OnInit()
    {
        base.OnInit();
        isCanMove = true;
        timeReload = weapon.timeBettwenShoot;
        timeCoolDown = 10;
        healthBar.UpDateHealthBar(maxHp, hp);
        if (weapon == null)
        {
            weapon = weapons[0];
        }
    }
    public void Shoot()
    {
       weapon.damageWeapon = baseDame;
       weapon.Shooting(this, targetPoint);
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
    public override void OnAttack()
    {
        base.OnAttack();
        target = GetTargetInRange();
        if (target != null && !target.IsDead )//&& weapon.isCanAttack)
        {
            targetPoint = target.TF.position;
            
            //ChangeAnim(Constant.ANIM_ATTACK);
            Shoot();
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

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag(Constant.TAG_CHARACTER))
    //    {
    //        DealDamage(this.gameObject);
    //        healthBar.UpDateHealthBar(maxHp, hp);
    //    }
    //}

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
