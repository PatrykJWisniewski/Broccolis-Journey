using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public int health = 100;
    public GameObject deathEffect;
    Rigidbody2D rb;
    Rigidbody2D prb;
    private float direction;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage (int damage)
    {
        GameObject Player = GameObject.Find("Player");
        health -= damage;
        prb = Player.GetComponent<Rigidbody2D>();
        direction = prb.position.x - rb.position.x;
        if (health <= 0)
        {
            Die();
        }
        else
        {
            enemyAI ai = GetComponent<enemyAI>();
            JumperAI jai = GetComponent<JumperAI>();


            if (ai != null)
            {
                if(direction > 0)
                {
                    rb.AddForce(new Vector2(-1500f, 1200f));
                }
                else
                {
                    rb.AddForce(new Vector2(1500f, 1200f));
                }
            }
            else if(jai != null)
            {
                if (direction > 0)
                {
                    rb.AddForce(new Vector2(-2000f, 1000f));
                }
                else
                {
                    rb.AddForce(new Vector2(2000f, 1000f));
                }
            }
            else
            {
                if (direction > 0)
                {
                    rb.AddForce(new Vector2(-3000f, 1000f));
                }
                else
                {
                    rb.AddForce(new Vector2(3000f, 1000f));
                }
            }
        }
    }
    // Start is called before the first frame update
    void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
        
    }
}
