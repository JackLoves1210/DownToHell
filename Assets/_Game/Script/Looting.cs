using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Looting : MonoBehaviour
{
    [SerializeField] Transform player;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(Constant.TAG_EXP))
        {
            Debug.Log("exp");
            Exp exp = Cache.GetExp(other);
            exp.MoveToTaget(player.position);
        }
    }
}
