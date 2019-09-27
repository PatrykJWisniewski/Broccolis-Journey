using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public Animator animator;
    public float charSpeed;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
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
        else
        {
            //Moves the caracter with the ground
            transform.position += Vector3.left * 1 * Time.deltaTime;
        }
    }
}
