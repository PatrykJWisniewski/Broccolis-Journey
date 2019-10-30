using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAI : MonoBehaviour
{

    public Transform target;
    public Transform Enemy;
    public float speed = 200f;
    public Animator animator;
    public float k_GroundedRadius = .05f;
    private float dist;
    private bool m_Grounded = true;
    private Vector2 direction;
    public Collider2D collider2D;
    public float Range;
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
        bool wasGrounded = m_Grounded;
        m_Grounded = false;
        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        dist = target.position.x - rb.position.x;
        if (collider2D.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            direction = new Vector2(0f, 0f).normalized;
        }else if(Mathf.Abs(dist) < .2){
            direction = new Vector2(0f, 0f).normalized;
        }
        else if (Mathf.Abs(dist) > Range)
        {
            direction = new Vector2(0f, 0f).normalized;
        }
        else
        {
            direction = new Vector2(dist, 0f).normalized;
        }
        Vector2 force = direction * speed * Time.deltaTime;

        Vector2 velocity = rb.velocity;
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        if (m_Grounded)
        {
            rb.gravityScale = 0.3f;
            force.y = 0;
        }
        else
        {
            rb.gravityScale = 1;
            force.y = -10;
        }
        // If not a flyer, only apply velocity to the x axis
        rb.velocity = Vector2.SmoothDamp(rb.velocity,force, ref m_Velocity, m_MovementSmoothing);

        if (rb.velocity.x >= 0.01f)
        {
            Enemy.localScale = new Vector3(-5f, 5f, 1f);
        }
        else if (rb.velocity.x <= -.01f)
        {
            Enemy.localScale = new Vector3(5f, 5f, 1f);
        }
    }
}