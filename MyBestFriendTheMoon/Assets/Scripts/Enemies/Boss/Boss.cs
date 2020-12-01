using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Boss : Enemy
{
    public BossAction[] actions;
    private int currentAction;
    private float actionCounter;

    private float shotCounter;
    private Vector2 moveDirection;
    public Transform targetPosition;
    private bool move = true;
    public Rigidbody2D theRB;
    public Slider hpSlider;

    public int currentHealth;

    public BossSequence[] sequences;
    public int currentSequence;

    void Start()
    {
        hp = maxHp;
        hpSlider.maxValue = maxHp;
        hpSlider.value = maxHp;
        actionCounter = actions[currentAction].actionLength;
    }
    void Update()
    {
        if(move)
        {
            moveDirection = Vector2.zero;
            moveDirection = targetPosition.transform.position - transform.position;
            theRB.velocity = moveDirection * speed;
            if(transform.position.y <= targetPosition.transform.position.y + 0.1f)
            {
                move = false;
                moveDirection = Vector2.zero;
            }
        }
        if(actionCounter > 0 && !move)
        {
            actionCounter -= Time.deltaTime;

            if(actions[currentAction].shouldShootGun)
            {
                shotCounter -= Time.deltaTime;
                if(shotCounter <= 0)
                {
                    shotCounter = actions[currentAction].timeBetweenGunShots;

                    foreach(Transform transform in actions[currentAction].gunShotPoints)
                    {
                        Instantiate(actions[currentAction].gunItem, transform.position, transform.rotation);
                    }
                }
            }

            if(actions[currentAction].shouldShootPulse)
            {
                shotCounter -= Time.deltaTime;
                if(shotCounter <= 0)
                {
                    shotCounter = actions[currentAction].timeBetweenPulseShots;

                    foreach (Transform transform in actions[currentAction].pulseShotPoints)
                    {
                        Instantiate(actions[currentAction].pulseItem, transform.position, transform.rotation);
                    }
                }
            }
        }
        else 
        {
            currentAction++;
            if(currentAction >= actions.Length)
            {
                currentAction = 0;
            }

            actionCounter = actions[currentAction].actionLength;
        }
    }
    public override void Die()
    {
        var scoreUI = FindObjectOfType<UIController>();
        scoreUI.score += scoreValue;
        scoreUI.UpdateScore();
        hpSlider.gameObject.SetActive(false);
        Destroy(gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public override void TakeDamage(int damage)
    {
        hp -= damage;
        hpSlider.value = hp;
        animator.SetTrigger("Hurt");

        if (hp <= 0)
        {
            Die();
        }
    }
}

[System.Serializable]
public class BossAction
{
    [Header("Action")]
    public float actionLength;

    public bool shouldMove;
    public float moveSpeed;
    public bool moveToPoint;
    public Transform target;

    public bool shouldShootGun;
    public GameObject gunItem;
    public float timeBetweenGunShots;
    public Transform[] gunShotPoints;

    public bool shouldShootPulse;
    public GameObject pulseItem;
    public float timeBetweenPulseShots;
    public Transform[] pulseShotPoints;
}

[System.Serializable]
public class BossSequence
{
    [Header("Sequence")]
    public BossAction[] actions;

    public int endSequenceHealth;
}
