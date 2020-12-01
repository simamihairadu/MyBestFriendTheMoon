using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missle : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public float speed;
    public float launchSpeed;
    public float startFollowDelay;
    bool follow = true;
    void Start()
    {
        rb2d.AddForce(transform.right* launchSpeed);
        while(startFollowDelay <= 0)
        {
            startFollowDelay -= Time.deltaTime;
        }
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        startFollowDelay -= Time.deltaTime;
        if (startFollowDelay <= 0 && follow)
        {
            var target = FindObjectOfType<Player>();
            Vector2 offset = target.transform.position - transform.position;
            float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            rb2d.AddForce((target.transform.position - transform.position).normalized * speed);
            follow = false;
        }
    }
}
