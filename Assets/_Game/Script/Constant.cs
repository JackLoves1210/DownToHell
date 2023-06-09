using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Constant
{
    public const string ANIM_IDLE = "idle";
    public const string ANIM_RUN = "run";
    public const string ANIM_ATTACK = "attack";
    public const string ANIM_VICTORY = "victory";
    public const string ANIM_DEATH = "dead";
    public const string ANIM_ULTI = "ulti";

    public const string TAG_CHARACTER = "Character";
    public const string TAG_EXP = "Exp";
    public const string TAG_BOT = "Bot";
    public const string TAG_PLAYER = "Player";
    public const string TAG_BLOCK = "Block";
    public const string TAG_BULLET_BOT = "BulletBot";

    public const string SOUND_ELECTRIC = "Electric";
    public const string SOUND_ATOMIC = "Atomic";
    public const string SOUND_LOST = "Lost";
    public const string SOUND_DIE = "Die";
    public const string SOUND_LEVELUP = "Levelup";

    public const int FRIST_INDEX = 0;
    public const int STAT_GROWTH = 5;

}
public enum WeaponType
{
    Gun = PoolType.Gun,
    Cone = PoolType.Cone,
    Circle = PoolType.Circle,
}

public enum StateGame { Mainmenu, GamePlay }
public enum BulletType
{
    MachineGun = PoolType.MachineGun,
    ShootGon = PoolType.ShootGon,
    Sniper = PoolType.Sniper,
    FlameThrower = PoolType.FlameThrower,
    Acid = PoolType.Acid,
    ElectricZone = PoolType.ElectricZone,
    AtomicZone = PoolType.AtomicZone,
    BotBullet = PoolType.BotBullet,
}