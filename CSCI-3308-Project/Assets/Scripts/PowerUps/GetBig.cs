using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GetBig : MonoBehaviour
{
    [SerializeField]
    private float Duration;

    [SerializeField]
    private float SizeMultiplier;


    Collider2D player;
    bool pow = false;

    public GameObject pickupEffect;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            player = collision;
            PickUp();
        }
    }

    void PickUp()
    {
        Instantiate(pickupEffect, transform.position, transform.rotation);

        player.transform.localScale *= SizeMultiplier;

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        pow = true;
    }

    private void Update()
    {
        if (pow == true)
        {
            Duration -= Time.deltaTime;
            if (Duration <= 0)
            {
                player.transform.localScale /= SizeMultiplier;
                Destroy(gameObject);
            }
        }
            
    }
}
