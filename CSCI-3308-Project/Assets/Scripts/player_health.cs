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

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0f)
        {
            Die();
        }
    }

    private void Update()
    {
        if(health <= 0f)
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
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(1);
    }

}
