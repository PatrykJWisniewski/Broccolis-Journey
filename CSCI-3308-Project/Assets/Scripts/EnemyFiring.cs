using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFiring : MonoBehaviour
{
    private float nextFire = 0.0f;
    public bool canShoot;
    public float fireRate;
    public Transform firePoint;
    public Transform target;
    public Transform Enemy;
    public enemyBullet Enemybullet;
    private Vector2 dir;
    public float bulletRange;
    private string name;

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(target.transform.position.x - Enemy.transform.position.x) < bulletRange)
        {
            if (canShoot)
            {
                dir = (new Vector3(target.position.x + 0f, target.position.y + 1, 0f) - Enemy.position).normalized;
                if (Time.time > nextFire)
                {
                    Shoot();
                    nextFire = Time.time + fireRate;
                }
            }
        }
        else
        {

        }
    }
    void Shoot()
    {
        name = gameObject.name;
        enemyBullet mybullet = Instantiate(Enemybullet, firePoint.position, firePoint.rotation);
        mybullet.direction = dir;
        mybullet.name = name;
    }
}
