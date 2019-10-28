using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubCharacterController : MonoBehaviour
{
    public static HubCharacterController S;

    private Animator animator;
    public float charSpeed;
    public bool openShop;
    public HubCharacters hubChar;

    // Start is called before the first frame update
    void Start()
    {
        S = this;
        animator = GetComponent<Animator>(); //Gets the animator off the character object in game
    }

    // Update is called once per frame
    void Update()
    {
        if (Main.S.shopMenu.activeInHierarchy == true) return;

        //If the player start to move too the right
        if (Input.GetKeyDown(KeyCode.D))
        {
            animator.Play("Right - Walking", 0);
        }
        //If the player starts to move too the left
        else if (Input.GetKeyDown(KeyCode.A))
        {
            animator.Play("Left - Walking", 0);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            animator.Play("Back - Walking", 0);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            animator.Play("Front - Walking", 0);
        }
        //If the player stops moving too the right
        else if (Input.GetKeyUp(KeyCode.D))
        {
            animator.Play("Right - Idle", 0);
        }
        //If the player stops moving too the left
        else if (Input.GetKeyUp(KeyCode.A))
        {
            animator.Play("Left - Idle", 0);
        }

        //If the player is moving too the right
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * charSpeed * Time.deltaTime;
        }
        //If the player is moving too the left
        else if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * charSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.up * charSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.down * charSpeed * Time.deltaTime;
        }

        if(Input.GetKeyDown(KeyCode.Return) && openShop)
        {
            Main.S.shopMenu.SetActive(true);
            hubChar.SetScale(new Vector3(0,0,0));
            Main.S.shopMenu.GetComponent<Shop_Menu>().SetupShop(hubChar.shopItems);
        }
    }

    //When the character runs into a trigger that was set up in the game scene
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If the thing it collied with has the tag finished
        if (collision.tag == "LoadPlatformer")
        {
            Main.S.PlatformerFailed(); //Funs a function in the main script
        }

        if(collision.tag == "Shopkeep")
        {
            Debug.Log("Can talk too a shopkeep");
            openShop = true;
            hubChar = collision.GetComponent<HubCharacters>();
        }
    }
}
