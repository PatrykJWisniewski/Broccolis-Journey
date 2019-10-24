using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    public float runSpeed = 40f;
    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
    public float crouchtime;
    private float limit = 0f;
    private float Cooldown = 0f;
    public float SlideLimit;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("speed", Mathf.Abs(horizontalMove));
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("isjumping", true);
        }
        if (Input.GetButtonDown("Crouch") && Time.time > Cooldown)
        {
            crouch = true;
            animator.SetBool("isCrouching", true);
            Cooldown = Time.time + SlideLimit;
            limit = Time.time + crouchtime;
        }
        if (Time.time > limit && crouch == true){
            crouch = false;
            animator.SetBool("isCrouching", false);
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
            animator.SetBool("isCrouching", false);
        }
    }
    public void OnLanding()
    {
        animator.SetBool("isjumping", false);
        animator.SetBool("isGrounded", true);
    }
    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }
}
