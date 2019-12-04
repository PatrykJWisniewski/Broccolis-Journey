using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBarrier : MonoBehaviour
{

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            LayerTrigger(collision);
        }
    }

    void LayerTrigger(Collider2D player)
    {
        player.GetComponent<player_health>().Die();
    }
}
