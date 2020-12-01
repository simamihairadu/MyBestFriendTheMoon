using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGun : MonoBehaviour
{
    public GameObject bullet;
    public Transform firePoint;
    public float fireRate;
    float fireTimer;

    void Update()
    {
        Fire();
    }
    void Fire()
    {
        fireTimer -= Time.deltaTime;
        if (fireTimer <= 0)
        {
            Instantiate(bullet, firePoint.position, firePoint.rotation);
            fireTimer = fireRate;
        }
    }
}
