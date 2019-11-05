using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    public static CharacterController2D S;

    [SerializeField] private float m_JumpForce = 400f;                          // Amount of force added when the player jumps.
    [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;          // Amount of maxSpeed applied to crouching movement. 1 = 100%
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
    [SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
    [SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings
    [SerializeField] private Collider2D m_CrouchDisableCollider;
    public Collider2D collider2D1;
    public Collider2D collider2D2;

    public float k_GroundedRadius = .05f; // Radius of the overlap circle to determine if grounded
    private bool m_Grounded;            // Whether or not the player is grounded.
    const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
    public Rigidbody2D m_Rigidbody2D;
    public static bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private Vector3 m_Velocity = Vector3.zero;
    private float delay = 0.0f;
    public float delaytime;
    public Animator animator;
    public GameObject player;
    public int dash_dist;
    public float parryForce;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    public BoolEvent OnCrouchEvent;
    private bool m_wasCrouching = false;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

        if (OnCrouchEvent == null)
            OnCrouchEvent = new BoolEvent();
    }

    private void Start()
    {
        S = this;
    }

    private void FixedUpdate()
    {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;
        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        if (Time.time > delay)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                {
                    m_Grounded = true;
                    if (!wasGrounded)
                        OnLandEvent.Invoke();
                }
            }
        }
        if (!m_Grounded)
        {
            animator.SetBool("isGrounded", false);
        }
    }


    public void Move(float move, bool crouch, bool jump, bool swit,float t1,bool parry)
    {
        // If crouching, check to see if the character can stand up
        if (!crouch)
        {
            // If the character has a ceiling preventing them from standing up, keep them crouching
            if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
            {
                crouch = true;
            }
        }

        //only control the player if grounded or airControl is turned on
        if (m_Grounded || m_AirControl)
        {
            // If crouching
            if (crouch)
            {
                if (!m_wasCrouching)
                {
                    m_wasCrouching = true;
                    OnCrouchEvent.Invoke(true);
                }

                // Reduce the speed by the crouchSpeed multiplier
                move *= m_CrouchSpeed;

                // Disable one of the colliders when crouching
                if (m_CrouchDisableCollider != null)
                    m_CrouchDisableCollider.enabled = false;
            }
            else
            {
                // Enable the collider when not crouching
                if (m_CrouchDisableCollider != null)
                    m_CrouchDisableCollider.enabled = true;

                if (m_wasCrouching)
                {
                    m_wasCrouching = false;
                    OnCrouchEvent.Invoke(false);
                }
            }

            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
            // And then smoothing it out and applying it to the character
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

            // If the input is moving the player right and the player is facing left...
            if (move > 0 && !m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
            if (swit && Time.time > t1 + this.animator.GetCurrentAnimatorStateInfo(0).length-.02)
            {
                if (m_FacingRight)
                {
                    if (Physics2D.OverlapCircle(player.transform.position + new Vector3(dash_dist, 1,0),0.5f) != null)
                    {
                        if (Physics2D.OverlapCircle(player.transform.position + new Vector3(dash_dist, 4, 0), 0.3f) != null) {
                            if (Physics2D.OverlapCircle(player.transform.position + new Vector3(dash_dist, -2, 0), 0.3f) != null) { }
                            else
                            {
                                player.transform.position = player.transform.position + new Vector3(dash_dist, -3, 0);
                            }
                        }
                        else
                        {
                            player.transform.position = player.transform.position + new Vector3(dash_dist, 3, 0);
                        }
                    }
                    else
                    {
                        player.transform.position = player.transform.position + new Vector3(dash_dist, 0, 0);
                    }
                }
                else if (!m_FacingRight)
                {
                    if (Physics2D.OverlapCircle(player.transform.position + new Vector3(-dash_dist, 1, 0), 0.5f) != null)
                    {
                        if (Physics2D.OverlapCircle(player.transform.position + new Vector3(-dash_dist, 4, 0), 0.3f) != null)
                        {
                            if (Physics2D.OverlapCircle(player.transform.position + new Vector3(-dash_dist, -2, 0), 0.3f) != null) { }
                            else
                            {
                                player.transform.position = player.transform.position + new Vector3(-dash_dist, -3, 0);
                            }
                        }
                        else
                        {
                            player.transform.position = player.transform.position + new Vector3(-dash_dist, 3, 0);
                        }
                    }
                    else
                    {
                        player.transform.position = player.transform.position + new Vector3(-dash_dist, 0, 0);
                    }
                }
            }
        }
        // If the player should jump...
        if (m_Grounded && jump)
        {
            // Add a vertical force to the player.
            m_Grounded = false;
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            delay = delaytime + Time.time;
        }
        if (!m_Grounded && m_Rigidbody2D.velocity.y < 0)
        {
            animator.SetBool("isFalling", true);
        }
        if (parry)
        {
            if (collider2D1.IsTouchingLayers(LayerMask.GetMask("Enemy")) || collider2D2.IsTouchingLayers(LayerMask.GetMask("Enemy")))
            {
                m_Rigidbody2D.AddForce(new Vector2(0, parryForce - (m_Rigidbody2D.velocity.y / Time.fixedDeltaTime)));
            }
        }

    }


    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name.Equals("Cloud"))
            this.transform.parent = col.transform;
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.name.Equals("Cloud"))
            this.transform.parent = null;
    }
}
