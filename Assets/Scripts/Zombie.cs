using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ZombieState
{
    Move,
    Eat,
    Die,
    Pause
}


public class Zombie : MonoBehaviour
{
    private ZombieState zombieState = ZombieState.Move;

    private Rigidbody2D rgd;
    public float moveSpeed = 1.5f;
    private Animator anim;

    public int atkValue = 30;
    public float atkDuration = 2;
    private float atkTimer = 0;

    private Plant currentEatPlant;

    public float HP = 100;
    private float currentHP;

    public GameObject zombieLostHeadPrefab;

    private bool haveHead = true;

    // Start is called before the first frame update
    void Start()
    {
        rgd = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentHP = HP;

    }

    // Update is called once per frame
    void Update()
    {
        switch(zombieState)
        {
            case ZombieState.Move:
                MoveUpdate();
                break;
            case ZombieState.Eat:
                EatUpdate();
                break;
            case ZombieState.Die:
                break;
            default:
                break;
        }
    }

    private void MoveUpdate()
    {
        rgd.MovePosition(rgd.position + Vector2.left * moveSpeed * Time.deltaTime);
    }

    private void EatUpdate()
    {
        atkTimer += Time.deltaTime;
        if (atkTimer >= atkDuration && currentEatPlant != null)
        {
            AudioManager.Instance.PlayClip(Config.eat);
            currentEatPlant.TaskDamage(atkValue);
            atkTimer = 0;
        }

    }


    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Plant")
        {
            anim.SetBool("IsAttacking", true);
            TransitionToEat();
            currentEatPlant = other.GetComponent<Plant>();
        } else if (other.tag == "House")
        {
            GameManager.Instance.GameEndFail();
        }
    }

    /// <summary>
    /// Sent when another object leaves a trigger collider attached to
    /// this object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Plant")
        {
            anim.SetBool("IsAttacking", false);
            print("OnTriggerExit2D");
            if (currentHP > 0)
            {
                zombieState = ZombieState.Move;
                currentEatPlant = null;
            }

        }
    }

    void TransitionToEat()
    {
        zombieState = ZombieState.Eat;
        atkTimer = 0;
    }

    public void TransitionToPause()
    {
        zombieState = ZombieState.Pause;
        anim.enabled = false;
    }

    public void TaskDamage(int damage)
    {
        if (currentHP <= 0) return;
        this.currentHP -= damage;
        if (currentHP <= 0)
        {
            currentHP = -1;
            Dead();
        }
        float hpPercent = currentHP*1f / HP;
        anim.SetFloat("HPPercent", hpPercent);
        if (hpPercent < 0.5f && haveHead)
        {
            haveHead = false;
            GameObject go = GameObject.Instantiate(zombieLostHeadPrefab, transform.position, Quaternion.identity);
            Destroy(go, 2);
        }

    }

    private void Dead()
    {
        if (zombieState == ZombieState.Die) return;
        zombieState = ZombieState.Die;
        GetComponent<Collider2D>().enabled = false;
        ZombieManager.Instance.RemoveZombie(this);

        Destroy(gameObject, 2);
    }
}
