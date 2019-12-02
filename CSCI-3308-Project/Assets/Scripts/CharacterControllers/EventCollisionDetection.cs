using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EventCollisionDetection : MonoBehaviour
{
    public static EventCollisionDetection S;

    public bool loadingHiddenBoss;
    public bool canOpenShop;

    private void Start()
    {
        S = this;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Coins")
        {
            Tilemap coins = collision.GetComponent<Tilemap>();

            if (coins.GetTile(coins.WorldToCell(transform.position + new Vector3(0, 1, 0))) != null)
            {
                PlatformerUI.S.UpdateMoneyTextAndAmount(1);
            }

            coins.SetTile(coins.WorldToCell(transform.position + new Vector3(0, 1, 0)), null);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "LoadHiddenBoss")
        {
            if (loadingHiddenBoss) return;
            loadingHiddenBoss = true;
            Main.S.StartCoroutine("LoadHiddenBossLevel");
        }

        if (collision.tag == "Shopkeep")
        {
            canOpenShop = true;
        }
    }

    private void OnTriggerExit2D(Collider collision)
    {
        if (collision.tag == "Shopkeep")
        {
            canOpenShop = false;
        }
    }
}
