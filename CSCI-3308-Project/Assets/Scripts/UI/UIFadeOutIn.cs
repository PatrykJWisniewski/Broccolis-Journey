using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFadeOutIn : MonoBehaviour
{
    public static UIFadeOutIn S;

    public float lerpTime;
    public float currentLerpTime;
    public float newTranparency;

    private void Start()
    {
        S = this;
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
        Color c = GetComponent<Image>().color;
        c.a = Mathf.Lerp(c.a, newTranparency, perc);
        GetComponent<Image>().color = c;
    }
}
