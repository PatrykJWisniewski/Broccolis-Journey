using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperBullet : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D rb;
    public int damage = 40;
    public GameObject impactEffect;
    public float delay = 0f;
    internal string name;
    JumperAI sender;
    private Vector2 new_direction;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
        GameObject EN = GameObject.Find(name);
        sender = EN.GetComponent<JumperAI>();
        Destroy(gameObject, delay);
    }
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        player_health player_health = hitInfo.GetComponent<player_health>();
        player_movement Player_move = hitInfo.GetComponent<player_movement>();
        CharacterController2D Player = hitInfo.GetComponent<CharacterController2D>();
        if (player_health != null)
        {
            if (!Player_move.parry)
            {
                player_health.TakeDamage(damage);
            }
            else if (Player_move.parry)
            {
                new_direction = (sender.Enemy.position - new Vector3(rb.position.x, rb.position.y, 0)).normalized;
                Player.m_Rigidbody2D.AddForce(new Vector2(0, 550 - (Player.m_Rigidbody2D.velocity.y / Time.fixedDeltaTime)));
                rb.AddForce(new_direction * 2 * speed, ForceMode2D.Impulse);
                gameObject.layer = 0;
            }
        }
        enemy enemy = hitInfo.GetComponent<enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        if (Player_move == null)
        {
            Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        else
        {
            if (!Player_move.parry)
            {
                Instantiate(impactEffect, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }

    }
}
