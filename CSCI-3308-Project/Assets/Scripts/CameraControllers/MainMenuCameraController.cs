using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCameraController : MonoBehaviour
{
    public static MainMenuCameraController S;

    public GameObject[] arrayOfBackgrounds;
    public GameObject[] arrayOfForegrounds;
    public GameObject[] ground;

    // Start is called before the first frame update
    void Start()
    {
        S = this;
    }

    // Update is called once per frame
    void Update()
    {
        //Moves the tiles 
        //Vector3.Left is (-1,0,0) 
        //Time.deltaTime is being used in order to make the movmenet independt of the frame rate, so people move the same speed if they have 40 or 60 frame per second
        foreach(GameObject g in ground)
        {
            Vector3 groundPos = g.transform.position;
            groundPos += Vector3.left * Time.deltaTime;
            g.transform.position = groundPos;

            if (g.transform.position.x <= -65)
            {
                g.transform.position = new Vector3(65, 0, 0);
            }
        }

        //Moves the foreground tiles
        foreach (GameObject f in arrayOfForegrounds)
        {
            f.transform.position += Vector3.left * .2f * Time.deltaTime;

            if (f.transform.position.x <= -29.3f)
            {
                f.transform.position = new Vector3(58.6f, 1, 0);
            }
        }

        //Moves the background tiles
        foreach (GameObject b in arrayOfBackgrounds)
        {
            b.transform.position += Vector3.left * .1f * Time.deltaTime;

            if (b.transform.position.x <= -29.3f)
            {
                b.transform.position = new Vector3(58.6f, 1, 0);
            }
        }
    }
}
