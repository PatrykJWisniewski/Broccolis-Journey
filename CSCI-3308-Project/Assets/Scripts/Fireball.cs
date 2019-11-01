using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private ParticleSystem ps;
    public ParticleSystem childPS;
    public GameObject explosion;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    private void OnParticleCollision(GameObject other)
    {
        bool moduleEnabled = false;
        var emission = ps.emission; //Emision of fire stream partical effect
        var childEmission = childPS.emission; //Emmision of fire sparks partical effect
        emission.enabled = moduleEnabled;

        childEmission.enabled = moduleEnabled; //Disables emission
        var collision = ps.collision;
        collision.enabled = moduleEnabled;

        //Creates an explosion when it collides with the ground or character
        GameObject boom = Instantiate(explosion);
        boom.transform.position = transform.position;
    }
}
