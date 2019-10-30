using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    private float nextFire = 0.0f;
    public float fireRate;
    public int ammo = 10;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire && ammo > 0)
        {
            nextFire = Time.time + fireRate;
            Shoot();
            ammo--;
        }
    }
    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}