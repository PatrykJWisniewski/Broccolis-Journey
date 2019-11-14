using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_health : MonoBehaviour
{
    public Animator animator;
    public int health = 300;
    public GameObject deathEffect;
    private bool dead = false;
    private float t1;
    void FixedUpdate()
    {
        if (dead && Time.time > t1 + this.animator.GetCurrentAnimatorStateInfo(0).length)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }
    // Start is called before the first frame update
    void Die()
    {
        if (!dead)
        {
            animator.SetBool("isDead", true);
            t1 = Time.time;
            dead = true;
        }
    }
}
