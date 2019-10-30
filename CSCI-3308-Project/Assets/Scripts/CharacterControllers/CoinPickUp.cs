using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CoinPickUp : MonoBehaviour
{
    //When the character has a collision
    private void OnCollisionStay2D(Collision2D collision)
    {
        //If the collision object has the tag Coins.
        if (collision.collider.tag == "Coins")
        {
            ContactPoint2D[] contacts = new ContactPoint2D[20]; //Create a new array for 20 contact points
            collision.GetContacts(contacts); //Get all the contact points
            Tilemap coins = collision.collider.GetComponent<Tilemap>();

            //Get rid of the coin at the first contact point
            coins.SetTile(coins.WorldToCell(contacts[0].point), null);
            PlatformerUI.S.UpdateMoneyTextAndAmount(1);

            //Get rid of all the coins at the other contact points
            for (int i = 1; i < contacts.Length; i++)
            {
                //If there was no contact...
                if (contacts[i].point == new Vector2(0, 0)) return;
                //If the coin at this contact point was already accounted for...
                if (coins.GetTile(coins.WorldToCell(contacts[i].point)) != null)
                {
                    coins.SetTile(coins.WorldToCell(contacts[i].point), null);
                    PlatformerUI.S.UpdateMoneyTextAndAmount(1);
                }
            }
        }
    }
}
