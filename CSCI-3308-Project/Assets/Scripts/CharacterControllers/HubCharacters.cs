using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubCharacters : MonoBehaviour
{
    private Animator animator;
    public GameObject triggerFlavorText;

    public float lerpTime;
    public float currentLerpTime;
    public Vector3 newScale;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play("Idle", 0);
        animator.speed = .5f;
    }

    private void Update()
    {
        //Increment timer once per frame
        currentLerpTime += Time.unscaledDeltaTime;
        if (currentLerpTime > lerpTime)
        {
            currentLerpTime = lerpTime;
        }

        //Moves in a smooth motion
        float perc = currentLerpTime / lerpTime;
        perc = perc * perc * (3f - 2f * perc); //Smooth lerp curve
        triggerFlavorText.transform.localScale = Vector3.Lerp(triggerFlavorText.transform.localScale, newScale, perc);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        newScale = new Vector3(1,1,1);
        currentLerpTime = 0;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        newScale = new Vector3(0, 0, 0);
        currentLerpTime = 0;
    }
}
