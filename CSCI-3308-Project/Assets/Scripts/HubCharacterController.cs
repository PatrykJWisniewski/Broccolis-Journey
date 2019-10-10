using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubCharacterController : MonoBehaviour
{
    private Animator animator;
    public float charSpeed;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); //Gets the animator off the character object in game
    }

    // Update is called once per frame
    void Update()
    {
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
    }

    //When the character runs into a trigger that was set up in the game scene
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If the thing it collied with has the tag finished
        if (collision.tag == "Finish")
        {
            Main.S.PlatformerFailed(); //Funs a function in the main script
        }
    }
}
