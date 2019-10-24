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
    // Start is called before the first frame update
    void Start()
    {
        bool m_facingright = gameObject.GetComponent<CharacterController2D>().m_FacingRight;
        if (m_facingright)
        {
            rb.velocity = transform.right * speed;
        }
        else if (!m_facingright)
        {
            rb.velocity = -transform.right * speed;
        }
        Destroy(gameObject, delay);
    }
    void OnTriggerEnter2D (Collider2D hitInfo)
    {
        enemy enemy = hitInfo.GetComponent<enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
    
}
