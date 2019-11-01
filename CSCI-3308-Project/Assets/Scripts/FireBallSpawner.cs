using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallSpawner : MonoBehaviour
{
    public GameObject fireBall;
    public Vector2 xRange;
    public Vector2 yRange;
    public int startTime;

    private void Start()
    {
        InvokeRepeating("SpawnFireBall", startTime, 4);
    }

    private void SpawnFireBall()
    {
        Vector3 velocity = new Vector3(Random.Range(xRange.x, xRange.y), Random.Range(yRange.x, yRange.y));

        GameObject go = Instantiate(fireBall);
        go.transform.position = transform.position;
        go.GetComponent<Rigidbody2D>().velocity = velocity;
        go.transform.rotation = Quaternion.LookRotation(-velocity);

        Destroy(go, 15);
    }
}
