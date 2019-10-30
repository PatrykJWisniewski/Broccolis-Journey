using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperAI : MonoBehaviour
{
    public Transform target;
    public Transform Enemy;
    public float speed = 200f;
    public float jumpSpeed = 1f;
    public Animator animator;
    public float k_GroundedRadius = .05f;
    private float dist;
    private bool m_Grounded = true;
    private Vector2 direction;
    public Collider2D collider2D;
    public float Range;
    public Transform firePoint;
    private float jumpDelay = 0f;
    public float JumpCooldown;
    public JumperBullet Jumperbullet;
    private bool wasJumping;
    private Vector2 jumpdir;
    private string name1;
    [SerializeField] private LayerMask m_WhatIsGround;
    [SerializeField] private Transform m_GroundCheck;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;

    private Vector2 m_Velocity = Vector2.zero;

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        m_Grounded = false;
        animator.SetBool("isGrounded", false);
        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                animator.SetBool("isGrounded", true);
                if (wasJumping)
                {
                    name1 = gameObject.name;
                    JumperBullet mybullet = Instantiate(Jumperbullet, firePoint.position, firePoint.rotation);
                    mybullet.name = name1;
                    firePoint.transform.Rotate(0, 0, 90);
                    JumperBullet mybullet1 = Instantiate(Jumperbullet, firePoint.position, firePoint.rotation);
                    mybullet1.name = name1;
                    firePoint.transform.Rotate(0, 0, 90);
                    JumperBullet mybullet2 = Instantiate(Jumperbullet, firePoint.position, firePoint.rotation);
                    mybullet2.name = name1;
                    firePoint.transform.Rotate(0, 0, 180);
                    wasJumping = false;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        dist = target.position.x - rb.position.x;
        if (m_Grounded)
        {
            if (collider2D.IsTouchingLayers(LayerMask.GetMask("Player")))
            {
                direction = new Vector2(0f, 0f).normalized;
            }
            else if (Mathf.Abs(dist) < .2)
            {
                direction = new Vector2(0f, 0f).normalized;
            }
            else if (Mathf.Abs(dist) > Range)
            {
                direction = new Vector2(0f, 0f).normalized;
            }
            else if (Time.time > jumpDelay)
            {
                rb.AddForce(new Vector2(-1, jumpSpeed), ForceMode2D.Impulse);
                direction = new Vector2(0f, 0f);
                jumpDelay = JumpCooldown + Time.time;
                jumpdir = new Vector2(dist, 0f).normalized;
            }
            else
            {
                direction = new Vector2(0f, 0f).normalized;
            }
        }
        else if (!m_Grounded)
        {
            direction = jumpdir;
            wasJumping = true;
        }
        Vector2 force = direction * speed * Time.deltaTime;

        if (rb.velocity.y < 0)
        {
            force.y = force.y - 10;
        }
        Vector2 velocity = rb.velocity;
        rb.velocity = Vector2.SmoothDamp(rb.velocity, force, ref m_Velocity, m_MovementSmoothing);
    }
}
