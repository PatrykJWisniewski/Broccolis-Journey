using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapin : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    private float nextFire = 0.0f;
    public int fireRate;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Shoot();
        }
    }
    void Shoot()
    { 
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
