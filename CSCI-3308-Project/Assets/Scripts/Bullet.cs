using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public int damage = 40;
    public GameObject impactEffect;
    public float delay = 0f;
    private bool right = true;
    internal string name;
    // Start is called before the first frame update
    void Start()
    {
        if (CharacterController2D.m_FacingRight)
        {
            if (!right)
            {
                Vector3 theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;
                right = true;
            }
            rb.velocity = transform.right * speed;
        }
        else if (!CharacterController2D.m_FacingRight)
        {
            if (right)
            {
                Vector3 theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;
                right = false;
            }
            rb.velocity = -transform.right * speed;
        }
        Destroy(gameObject, delay);
    }
    void OnTriggerEnter2D (Collider2D hitInfo)
    {
        if (hitInfo.tag == "Coins") return;

        enemy enemy = hitInfo.GetComponent<enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        player_health player_health = hitInfo.GetComponent<player_health>();
        if (player_health == null)
        {
            Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
    
}
