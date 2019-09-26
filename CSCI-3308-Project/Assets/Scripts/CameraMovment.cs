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
    void FixedUpdate()
    {
        //Moves the ground tiles
        foreach (GameObject g in arrayOfGround)
        {
            Vector3 groundPos = g.transform.position;
            groundPos.x += -.015f;
            g.transform.position = groundPos;

            //Moves the ground tiles too the right of the camera if they have gone too far too the lefgt
            if (g.transform.position.x <= -10)
            {
                groundPos.x = 10;
                g.transform.position = groundPos;
            }
        }

        foreach (GameObject f in arrayOfForegrounds)
        {
            Vector3 forePos = f.transform.position;
            forePos.x += -.01f;
            f.transform.position = forePos;

            if (f.transform.position.x <= -32)
            {
                forePos.x = 16;
                f.transform.position = forePos;
            }
        }

        foreach (GameObject b in arrayOfBackgrounds)
        {
            Vector3 backPos = b.transform.position;
            backPos.x += -.005f;
            b.transform.position = backPos;

            if(b.transform.position.x <= -32)
            {
                backPos.x = 16;
                b.transform.position = backPos;
            }
        }
    }
}
