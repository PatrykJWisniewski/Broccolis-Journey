using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player_health : MonoBehaviour
{
    public float health = 2000f;
    public float originalHealth = 2000f;
    public GameObject deathEffect;
    public GameObject FailLevelUI;
    public Animator animator;
    private float t1;
    public Rigidbody2D m_Rigidbody2D;
    private float invul = 0f;
    public float recovery_time = .5f;
    public CharacterController2D char_con;

    public void TakeDamage(int damage)
    {
       
        if (!char_con.invulnerable)
        {
            m_Rigidbody2D.AddForce(new Vector2(-500 - (m_Rigidbody2D.velocity.x / Time.fixedDeltaTime), 700 - (m_Rigidbody2D.velocity.y / Time.fixedDeltaTime)));
            animator.SetBool("isHurt", true);
            char_con.hurt = true;
            char_con.invulnerable = true;
            invul = Time.time + .35f;
            health -= damage;
        }

        if (health <= 0f)
        {
            Die();
        }
    }

    private void Update()
    {
        if (char_con.invulnerable && Time.time > invul + recovery_time)
        {
            char_con.invulnerable = false;
            animator.SetBool("isHurt", false);
        }
        else if (char_con.hurt && Time.time > invul)
        {
            char_con.hurt = false;
            animator.SetBool("isHurt", false);
        }
        if (health <= 0f)
        {
           Die();
        }
    }
    public void Die()
    {
        //Instantiate(deathEffect, transform.position, Quaternion.identity);
        FailLevelUI.SetActive(true);
        StartCoroutine(Pause());
    }
    private IEnumerator Pause()
    {
        yield return new WaitForSeconds(2);

        if(Main.S.spooky)
        {
            SceneManager.LoadScene("SpookyHub");
        }
        else if (Main.S.forest)
        {
            SceneManager.LoadScene("ForestHub");
        }
    }
}
