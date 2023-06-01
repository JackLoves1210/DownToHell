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
           
            HandleEventLevelUp();
            Destroy(gameObject);
        }
    }

    public void HandleEventLevelUp()
    {
        UIManager.Ins.OpenUI<GamePlay>().Close(0);
        Time.timeScale = 0;
        UIManager.Ins.OpenUI<LevelUp>();
        
    }
 
}
