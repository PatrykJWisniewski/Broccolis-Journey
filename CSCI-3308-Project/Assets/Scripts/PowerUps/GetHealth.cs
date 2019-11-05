using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHealth : MonoBehaviour
{
    [SerializeField]
    private int HealthGiven;

    public GameObject pickupEffect;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PickUp(collision);
        }
    }

    void PickUp(Collider2D player)
    {
        Instantiate(pickupEffect, transform.position, transform.rotation);

        player_health stats = player.GetComponent<player_health>();
        stats.health += HealthGiven;

        Destroy(gameObject);
    }
}
