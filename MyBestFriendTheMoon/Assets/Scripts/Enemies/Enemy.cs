using Assets.Scripts.Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IEnemy
{
    public int hp;
    public int maxHp;
    public ItemDrop drop;
    public float speed;
    public int scoreValue;
    public Animator animator;

    void Start()
    {
        hp = maxHp;
    }
    public virtual void TakeDamage(int damage)
    {
        hp -= damage;

        animator.SetTrigger("Hurt");

        if (hp <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        int randomDrop = Random.Range(1, 10);
        if (randomDrop > 7)
        {
            Instantiate(drop.Drop(), transform.position, Quaternion.identity);
        }
        FindObjectOfType<MoonBehaviour>().ComboBoost();
        var scoreUI = FindObjectOfType<UIController>();
        scoreUI.score += scoreValue;
        scoreUI.UpdateScore();
        Destroy(gameObject);
    }
}
