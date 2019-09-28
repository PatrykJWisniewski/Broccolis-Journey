using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticalFunctions : MonoBehaviour
{
    private ParticleSystem ps;

    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>(); //Gets the partical system off the object this script is attached too
    }

    // Update is called once per frame
    void Update()
    {
        //If the partical system is no longer emiting particals
        if(!ps.IsAlive())
        {
            //Delete the object from the scene
            Destroy(gameObject);
        }
    }
}
