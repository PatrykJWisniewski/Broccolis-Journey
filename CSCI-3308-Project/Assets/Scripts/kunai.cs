using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kunai : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public int damage = 40;
    public float delay = 0f;
    // Start is called before the first frame update
    void Start()
    {
        if (CharacterController2D.m_FacingRight)
        {
            rb.velocity = transform.right * speed;
        }
        else if (!CharacterController2D.m_FacingRight)
        {
            rb.velocity = -transform.right * speed;
        }
        Destroy(gameObject, delay);
    }
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        enemy enemy = hitInfo.GetComponent<enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
