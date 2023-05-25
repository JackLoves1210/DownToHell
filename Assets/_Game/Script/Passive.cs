using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Passive 
{
    public passive passive;
    public float statGrowth;
    public int index = 1;
}

public enum passive {MaxHP,MoveSpeed,Might,Lifesteal}
