using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetSpeed : MonoBehaviour
{
    [SerializeField]
    private float Duration;

    [SerializeField]
    private float SpeedMultiplier;

    Collider2D player;
    bool pow = false;

    public GameObject pickupEffect;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PickUp(collision);
        }
    }

    void PickUp(Collider2D col)
    {
        Instantiate(pickupEffect, transform.position, transform.rotation);

        player = col;

        player_movement stats = player.GetComponent<player_movement>();
        stats.runSpeed *= SpeedMultiplier;

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        player.GetComponent<TrailRenderer>().enabled = true;

        pow = true;

    }

    private void Update()
    {
        if(pow == true)
        {
            Duration -= Time.deltaTime;
            if (Duration < 0)
            {
                player_movement stats = player.GetComponent<player_movement>();
                player.GetComponent<TrailRenderer>().enabled = false;

                stats.runSpeed /= SpeedMultiplier;
                pow = false;
                Destroy(gameObject);

            }
        }
        
    }
}
