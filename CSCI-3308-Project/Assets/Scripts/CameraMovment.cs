using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovment : MonoBehaviour
{
    public static CameraMovment S;

    public GameObject[] arrayOfBackgrounds;
    public GameObject[] arrayOfForegrounds;
    public GameObject[] arrayOfGround;

    // Start is called before the first frame update
    void Start()
    {
        S = this;
    }

    // Update is called once per frame
    void Update()
    {
        //Moves the ground tiles
        foreach (GameObject g in arrayOfGround)
        {
            //Moves the tiles 
            //Vector3.Left is (-1,0,0) 
            //Time.deltaTime is being used in order to make the movmenet independt of the frame rate, so people move the same speed if they have 40 or 60 frame per second
            g.transform.position += Vector3.left * 1 * Time.deltaTime;

            //Moves the ground tiles too the right of the camera if they have gone too far too the lefgt
            if (g.transform.position.x <= -10)
            {
                g.transform.position = new Vector3(10,-2.5f,0); 
            }
        }

        //Moves the foreground tiles
        foreach (GameObject f in arrayOfForegrounds)
        {
            f.transform.position += Vector3.left * .2f * Time.deltaTime;

            if (f.transform.position.x <= -16)
            {
                f.transform.position = new Vector3(31.5f, 1, 0);
            }
        }

        //Moves the background tiles
        foreach (GameObject b in arrayOfBackgrounds)
        {
            b.transform.position += Vector3.left * .1f * Time.deltaTime;

            if (b.transform.position.x <= -16)
            {
                b.transform.position = new Vector3(31.5f, 1, 0);
            }
        }
    }
}
