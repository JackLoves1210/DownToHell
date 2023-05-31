using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RamdomBot : Singleton<RamdomBot>
{
    public  Vector3 GetRandomPointOnNavMesh()
    {
        Vector3 randomPoint = Random.insideUnitSphere * 100f; 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 100f, NavMesh.AllAreas))
        {
            return hit.position;
        }
        return Vector3.zero;
    }
}
