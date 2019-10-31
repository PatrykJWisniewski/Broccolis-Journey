using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private ParticleSystem ps;
    public ParticleSystem childPS;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    private void OnParticleTrigger()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("coll");
        bool moduleEnabled = false;
        var emission = ps.emission;
        var childEmission = childPS.emission;
        emission.enabled = moduleEnabled;
        childEmission.enabled = moduleEnabled;
        var collision = ps.collision;
        collision.enabled = moduleEnabled;
    }
}
