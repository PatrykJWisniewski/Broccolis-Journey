using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CoinPickUp : MonoBehaviour
{
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
}
