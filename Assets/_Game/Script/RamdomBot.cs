using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RamdomBot : Singleton<RamdomBot>
{
    public  Vector3 GetRandomPointOnNavMesh()
    {
        Vector3 randomPoint = Random.insideUnitSphere * 100f; // Lấy một điểm ngẫu nhiên trong hình cầu bán kính 100
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 100f, NavMesh.AllAreas)) // Tìm kiếm điểm gần nhất trên NavMesh
        {
            return hit.position;
        }
        return Vector3.zero;
    }
}
