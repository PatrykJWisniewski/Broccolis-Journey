using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCharacters : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play("Walking", 0);
    }
}
