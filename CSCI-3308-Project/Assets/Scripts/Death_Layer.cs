using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Death_Layer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SpikeHit(collision);
        }
    }

    void SpikeHit(Collider2D player)
    {
        player_health stats = player.GetComponent<player_health>();
        stats.health -= 1000;
    }
}
