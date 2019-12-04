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
    public float Range;
    private string name;

    // Update is called once per frame
    void Update()
    {
        if (target.position.x - Enemy.position.x < Range|| target.position.y - Enemy.position.y < Range)
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
    }
    void Shoot()
    {
        name = gameObject.name;
        enemyBullet mybullet = Instantiate(Enemybullet, firePoint.position, firePoint.rotation);
        mybullet.direction = dir;
        mybullet.name = name;
    }
}
