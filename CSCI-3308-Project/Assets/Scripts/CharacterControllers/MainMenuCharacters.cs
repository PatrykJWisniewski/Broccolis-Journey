using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCharacters : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play("Running", 0);
        animator.speed = .5f;
    }
}
