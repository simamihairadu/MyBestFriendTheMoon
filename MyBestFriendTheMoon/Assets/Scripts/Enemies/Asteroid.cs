using Assets.Scripts.Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : Enemy
{
    Rigidbody2D rb2d;

    private void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        var target = FindObjectOfType<Player>();
        rb2d.AddForce((target.transform.position - transform.position).normalized * speed);
        Destroy(gameObject, 7f);
    }

    void Update()
    {
        //transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }
}
