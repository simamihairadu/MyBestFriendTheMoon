using Assets.Scripts.Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAlien : Enemy
{
    public float range;

    public GameObject bullet;
    public Transform firePoint;
    public float fireRate;
    float fireTimer;
    bool inRange = false;

    Player target;

    private void Start()
    {
        target = FindObjectOfType<Player>();
    }
    void Update()
    {
        FaceTarget();
        FollowTarget();
        Fire();
    }

    void FollowTarget()
    {
        if (Vector2.Distance(transform.position, target.transform.position) > range)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            inRange = false;
        }
        else
        {
            inRange = true;
        }
    }

    void FaceTarget()
    {
        if(target.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-0.3f, 0.3f, 0.7f);
        }
        else
        {
            transform.localScale = new Vector3(0.3f, 0.3f, 0.7f);
        }
    }

    void Fire()
    {
        fireTimer -= Time.deltaTime;
        if(inRange && fireTimer <= 0)
        {
            Instantiate(bullet, firePoint.position, firePoint.rotation);
            fireTimer = fireRate;
        }
    }
}
