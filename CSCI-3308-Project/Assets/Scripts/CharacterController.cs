using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (EdgeCollider2D))]
public class CharacterController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rigi;
    private EdgeCollider2D charCollider;
    private RaycastOrigins raycastOrigins;
    const float skinWidth = .015f;
    public int horizontalRayCount = 4;
    public int verticalRayCount = 4;
    private float horizontalRaySpacing;
    private float verticalRaySpacing;

    public float charSpeed;
    public Vector3 charJumpPos = new Vector3(0,1,0);
    public bool isGrounded;
    public GameObject nuke;

    struct RaycastOrigins
    {
        public Vector2 topLeft;
        public Vector2 topRight;
        public Vector2 bottomLeft;
        public Vector2 bottomRight;
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); //Gets the animator off the character object in game
        rigi = GetComponent<Rigidbody2D>(); //Gets the rigibody 2d componet off the character object in game
        charCollider = GetComponent<EdgeCollider2D>();
    }

    void UpdateRaycastOrigins()
    {
        Bounds bounds = charCollider.bounds;
        bounds.Expand(skinWidth * -2);

        raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    void CalculateRaySpacing()
    {
        Bounds bounds = charCollider.bounds;
        bounds.Expand(skinWidth * -2);

        horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
        verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);

        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRaycastOrigins();
        CalculateRaySpacing();

        for(int i = 0; i<verticalRayCount; i++)
        {
            Debug.DrawRay(raycastOrigins.bottomRight + Vector2.up * horizontalRaySpacing * i, Vector2.right * 2, Color.red);
            Debug.DrawRay(raycastOrigins.bottomLeft + Vector2.right * verticalRaySpacing * i, Vector2.up * -2, Color.red);
        }

        //If the player uses the Q key.
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(CameraMovment.S.newTimeScaleValue == 1)
            {
                CameraMovment.S.newTimeScaleValue = .25f;
                CameraMovment.S.currentLerpTime = 0;
            }
            else
            {
                CameraMovment.S.newTimeScaleValue = 1f;
                CameraMovment.S.currentLerpTime = 0;
            }
        }

        //If the player uses the E key.
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject go = Instantiate(nuke);
            go.transform.position = transform.position + Vector3.up;
        }


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

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rigi.AddForce(charJumpPos * 10, ForceMode2D.Impulse);
            isGrounded = false;
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
        else if (isGrounded)
        {
            //Moves the caracter with the ground
            transform.position += Vector3.left * 1 * CameraMovment.S.timeScaleValue * Time.deltaTime;
        }
    }

    //When the character runs into a trigger that was set up in the game scene
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If the thing it collied with has the tag finished
        if(collision.tag == "Finish")
        {
            Main.S.PlatformerFailed(); //Funs a function in the main script
        }
    }

    //When the chararcter touches the ground the bool isGrounded is set to true so they can jump again
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }
}
