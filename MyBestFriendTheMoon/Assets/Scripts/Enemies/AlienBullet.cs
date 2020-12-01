using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienBullet : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public float speed;
    void Start()
    {
        var target = FindObjectOfType<Player>();
        Vector2 offset = target.transform.position - transform.position;
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        rb2d.AddForce((target.transform.position - transform.position).normalized * speed);
        Destroy(gameObject, 20f);
    }
}
