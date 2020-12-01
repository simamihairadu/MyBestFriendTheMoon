using Assets.Scripts.Collectables;
using Assets.Scripts.Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int hp;
    public int maxHp;

    private void Start()
    {
        hp = maxHp;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Collectable"))
        {
            collision.gameObject.GetComponent<ICollectableItem>().Boost();
        }
        if(collision.CompareTag("Enemy"))
        {
            IEnemy enemy = collision.GetComponent<IEnemy>();
            if (enemy != null)
            {
                TakeDamage(1);
                enemy.Die();
            }
        }
        if(collision.CompareTag("Bullet"))
        {
            TakeDamage(1);
            Destroy(collision.gameObject);
        }
        if(collision.CompareTag("Boss"))
        {
            Die();
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        IEnemy enemy = collision.collider.GetComponent<IEnemy>();
        if (enemy != null)
        {
            TakeDamage(1);
            enemy.Die();
        }
    }

    void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Die();
        }
        var hpUI = FindObjectOfType<UIController>();
        hpUI.hpText.SetText(hp.ToString());
    }

    void Die()
    {
        FindObjectOfType<MoonBehaviour>().gameObject.GetComponent<MoonBehaviour>().enabled = false;
        FindObjectOfType<PlayerMovement>().gameObject.GetComponent<PlayerMovement>().enabled = false;
        FindObjectOfType<UIController>().resetUI.gameObject.SetActive(true);
        Cursor.visible = true;
        gameObject.SetActive(false);
    }
}
