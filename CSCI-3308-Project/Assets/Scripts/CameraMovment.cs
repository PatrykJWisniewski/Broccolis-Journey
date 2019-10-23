using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovment : MonoBehaviour
{
    public static CameraMovment S;

    public GameObject[] arrayOfBackgrounds;
    public GameObject[] arrayOfForegrounds;
    public GameObject ground;

    //Lerp varaiables
    public float currentLerpTime;
    public float lerpTime;
    public float newTimeScaleValue;
    public float timeScaleValue;

    // Start is called before the first frame update
    void Start()
    {
        S = this;
    }

    // Update is called once per frame
    void Update()
    {
        //Increment timer once per frame
        currentLerpTime += Time.unscaledDeltaTime;
        if (currentLerpTime > lerpTime)
        {
            currentLerpTime = lerpTime;
        }

        //Slows down time in a smooth motion
        float perc = currentLerpTime / lerpTime;
        perc = perc * perc * (3f - 2f * perc); //Smooth lerp curve
        timeScaleValue = Mathf.Lerp(timeScaleValue, newTimeScaleValue, perc);

        //Moves the tiles 
        //Vector3.Left is (-1,0,0) 
        //Time.deltaTime is being used in order to make the movmenet independt of the frame rate, so people move the same speed if they have 40 or 60 frame per second

        //Moves the foreground tiles
        foreach (GameObject f in arrayOfForegrounds)
        {
            f.transform.position += Vector3.left * .2f * timeScaleValue * Time.deltaTime;

            if (f.transform.position.x <= -29.3f)
            {
                f.transform.position = new Vector3(58.6f, 1, 0);
            }
        }

        //Moves the background tiles
        foreach (GameObject b in arrayOfBackgrounds)
        {
            b.transform.position += Vector3.left * .1f * timeScaleValue * Time.deltaTime;

            if (b.transform.position.x <= -29.3f)
            {
                b.transform.position = new Vector3(58.6f, 1, 0);
            }
        }
    }
}
