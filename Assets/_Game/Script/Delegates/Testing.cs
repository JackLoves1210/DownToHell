using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Testing : Singleton<Testing>
{
     UnityAction Action;

    private CounterTime counter = new CounterTime();
    public CounterTime Counter => counter;

    private void Start()
    {
        Test();
    }

    public void Update()
    {
        //Counter.Execute();
    }
    public void Test()
    {
        //Counter.Start(() => Debug.Log("Om e"),2f);
        
    }
}
