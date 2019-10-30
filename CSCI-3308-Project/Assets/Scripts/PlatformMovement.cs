using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 posA;
    private Vector3 posB;
    private Vector3 nexPos;
    [SerializeField]
    private float speed;
    void Start()
    {
        nexPos = posB;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
