using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestShotting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public void Update()
    {
        
       
    }
    public void ShootShotgun()
    {
        Vector3 shootDirection = transform.forward; // Hướng bắn chính

        for (int i = 0; i < 3; i++)
        {
            float spreadAngle = Random.Range(-15f, 15f); // Góc phân tán ngẫu nhiên

            Quaternion spreadRotation = Quaternion.Euler(0f, spreadAngle, 0f); // Quay hướng phân tán

            Vector3 bulletDirection = spreadRotation * shootDirection; // Hướng bắn của viên đạn

            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity); // Tạo viên đạn từ prefab
            bullet.GetComponent<Rigidbody>().AddForce(bulletDirection * 10f, ForceMode.Impulse); // Bắn viên đạn theo hướng đã tính toán
        }
    }
}
