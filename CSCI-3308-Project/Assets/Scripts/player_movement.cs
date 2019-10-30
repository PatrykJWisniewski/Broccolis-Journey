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
    bool dash = false;
    public bool parry = false;
    bool isjumping = false;
    public float crouchtime;
    private float limit = 0f;
    private float Cooldown = 0f;
    private float Cooldown2 = 0f;
    public float SlideLimit;
    public float DashLimit;
    private float parrytime;
    public float parryLimit;
    private float Cooldown3 = 0f;
    private float t1;
    private float t2;
    private bool swit = false;
    public Rigidbody2D m_Rigidbody2D;

    void Start()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("speed", Mathf.Abs(horizontalMove));
        if (Input.GetButtonDown("Jump")&& !isjumping)
        {
            isjumping = true;
            jump = true;
            animator.SetBool("isjumping", true);
        }
        else if (Input.GetButtonDown("Jump") && isjumping && Time.time > Cooldown3)
        {
            animator.SetBool("isParry", true);
            parry = true;
            parrytime = Time.time + .23f;
            Cooldown3 = parryLimit + Time.time;
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
        if (Input.GetKeyDown("e") && !crouch && Time.time > Cooldown2 && !parry)
        {
            animator.SetBool("isDashing", true);
            dash = true;
            m_Rigidbody2D.gravityScale = 1.5f;
            m_Rigidbody2D.drag = 1.5f;
            Cooldown2 = Time.time + DashLimit;
            t1 = Time.time;
            swit = true;
        }
        else if(Time.time > t1 + 2*this.animator.GetCurrentAnimatorStateInfo(0).length)
        {
            animator.SetBool("isDashing", false);
            m_Rigidbody2D.gravityScale = 3f;
            m_Rigidbody2D.drag = 0f;
            dash = false;
        }
        if(parry && Time.time > parrytime)
        {
            parry = false;
            animator.SetBool("isParry", false);
        }
    }
    public void OnLanding()
    {
        isjumping = false;
        animator.SetBool("isjumping", false);
        animator.SetBool("isGrounded", true);
        animator.SetBool("isFalling", false);
    }
    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump, swit, t1,parry);
        if (Time.time > t1 + this.animator.GetCurrentAnimatorStateInfo(0).length-.02)
        {
            swit = false;
        }
        jump = false;
    }
}
