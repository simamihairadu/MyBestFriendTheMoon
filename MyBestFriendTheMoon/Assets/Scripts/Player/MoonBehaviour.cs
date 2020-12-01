using Assets.Scripts.Enemies;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MoonBehaviour : MonoBehaviour
{
    public float xSpread;
    public float ySpread;
    public float maxRange;
    public float minRange;
    public Transform centerPoint;

    int enemyHits = 0;
    public int maxHitCombo = 3;
    public float hitComboSpeedValue = 0.5f;
    bool onFire = false;

    float comboExpireTimer = 0f;
    public float comboExpireTime = 3f;

    public float initialRotationSpeed;
    public float rotationSpeed;
    public float maxRotationSpeed;
    public float distanceIncreaseSpeed;
    float timer = 0f;
    void Update()
    {
        timer += Time.deltaTime * rotationSpeed;
        if(onFire)
            StartSpeedComboTimer();
        Rotate();
        CheckForIncreaseDistance();

    }
    void StartSpeedComboTimer()
    {
        comboExpireTimer += Time.deltaTime;
        if(comboExpireTimer > comboExpireTime)
        {
            rotationSpeed = initialRotationSpeed;
            comboExpireTimer -= comboExpireTimer;
            enemyHits = 0;
            onFire = false;
        }
    }
    void IncreaseSpeedByEnemiesHit()
    {
        rotationSpeed += hitComboSpeedValue;
    }
    void CheckForIncreaseDistance()
    {
        if(Input.GetMouseButton(0))
        {
            if(xSpread < maxRange)
            {
                xSpread += distanceIncreaseSpeed;
                ySpread += distanceIncreaseSpeed;
            }
        }
        else if(Input.GetMouseButton(1))
        {
            if (xSpread > minRange)
            {
                xSpread -= distanceIncreaseSpeed;
                ySpread -= distanceIncreaseSpeed;
            }
        }
    }
    void Rotate()
    {
        float x = -Mathf.Cos(timer) * xSpread;
        float y = Mathf.Sin(timer) * ySpread;
        Vector2 position = new Vector2(x, y);
        Vector2 center = new Vector2(centerPoint.position.x, centerPoint.position.y);
        transform.position = position + center;
        //if (timer > 6.3f) timer = 0;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        IEnemy enemy = collision.GetComponent<IEnemy>();
        Attack(enemy);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        IEnemy enemy = collision.collider.GetComponent<IEnemy>();
        Attack(enemy);
    }

    void Attack(IEnemy enemy)
    {
        if (enemy != null)
        {
            enemy.TakeDamage(1);
        }
    }

    public void ComboBoost() 
    {
        onFire = true;
        comboExpireTimer -= comboExpireTimer;
        if (enemyHits < maxHitCombo)
        {
            IncreaseSpeedByEnemiesHit();
            enemyHits++;
        }
    }
}
