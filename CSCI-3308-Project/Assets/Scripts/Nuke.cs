using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nuke : MonoBehaviour
{
    public GameObject explosionParticals;
    public Rigidbody2D rigi;

    // Start is called before the first frame update
    void Start()
    {
        rigi = GetComponent<Rigidbody2D>();
        rigi.AddForce(new Vector3(1,1,0) * 10, ForceMode2D.Impulse); //Adds force too the rigidbody making the object move in an arch
    }

    //When the objects hits something
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //If the object hit the ground
        if (collision.gameObject.tag == "Ground")
        {
            GameObject go = Instantiate(explosionParticals); //Create the explosion effect in the game
            go.transform.position = transform.position; //Set its posotion to be the same as the "nuke"
            Destroy(gameObject); //Destroy the nuke object
        }
    }

}
