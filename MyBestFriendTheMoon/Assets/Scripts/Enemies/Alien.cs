using Assets.Scripts.Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : Enemy
{
    void Update()
    {
        FollowTarget();
    }

    void FollowTarget()
    {
        var target = FindObjectOfType<Player>();
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }
}
