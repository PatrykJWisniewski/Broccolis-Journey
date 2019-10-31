using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallSpawner : MonoBehaviour
{
    public GameObject fireBall;

    private void Start()
    {
        InvokeRepeating("SpawnFireBall", 1, 2);
    }

    private void SpawnFireBall()
    {
        Vector3 velocity = new Vector3(Random.Range(2, 20), Random.Range(-10, -20));

        GameObject go = Instantiate(fireBall);
        go.transform.position = transform.position;
        go.GetComponent<Rigidbody2D>().velocity = velocity;
        go.transform.rotation = Quaternion.LookRotation(-velocity);

        Destroy(go, 15);
    }
}
