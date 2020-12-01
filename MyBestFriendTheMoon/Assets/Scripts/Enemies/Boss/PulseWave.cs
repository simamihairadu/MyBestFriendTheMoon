using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseWave : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public float launchSpeed;
    void Start()
    {
        rb2d.AddForce(transform.up * launchSpeed);
        Destroy(gameObject, 5f);
    }
}
