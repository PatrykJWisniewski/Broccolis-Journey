using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (null != animator)
            {
                animator.CrossFade("Moving Forward", 1);
                transform.eulerAngles = 0 * Vector3.up;
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (null != animator)
            {
                animator.CrossFade("Moving Forward", 1);
                transform.eulerAngles = 180 * Vector3.up;
            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (null != animator)
            {
                transform.position += new Vector3(.05f, 0, 0);
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            if (null != animator)
            {
                transform.position += new Vector3(-.05f, 0, 0);
            }
        }

        if (Input.GetKey(KeyCode.S))
        {
            if (null != animator)
            {
                transform.position += new Vector3(0, -.05f, 0);
            }
        }

        if (Input.GetKey(KeyCode.W))
        {
            if (null != animator)
            {
                transform.position += new Vector3(0, .05f, 0);
            }
        }

        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
        {
            if (null != animator)
            {
                animator.CrossFade("Idle", 1);
            }
        }
    }
}
