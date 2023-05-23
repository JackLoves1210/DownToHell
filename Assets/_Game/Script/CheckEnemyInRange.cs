using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEnemyInRange : MonoBehaviour
{
    [SerializeField] private Player player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAG_CHARACTER))
        {
            Character target = Cache.GetCharacter(other);
            if (!target.IsDead)
            {
                player.targets.Add(target);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constant.TAG_CHARACTER))
        {
            Character target = Cache.GetCharacter(other);
            player.targets.Remove(target);
        }
    }
}
