using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turret_script : MonoBehaviour
{
    public float range;

    public Transform Player;

    bool Detected = false;

    Vector2 Direction;

    public GameObject bullet;

    public float Firerate;

    float nextTimeToFire = 0;

    public Transform ShootPoint;

    public float force;

    // Update is called once per frame
    void Update()
    {
        float distToPlayer = Vector2.Distance(transform.position, Player.position);
        {
            if(distToPlayer <= range)
            {
               Detected = true;
                Debug.Log("true");

            }
            else
            {
                Detected = false;
    
            }
        }
        if(Detected==true)
        {
            if(Time.time > nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1 / Firerate;
                shoot();
            }
        }
    }
    void shoot()
    {
        //GameObject BulletIns = Instantiate(bullet, ShootPoint.position, Quaternion.identity);
        //BulletIns.GetComponent<Rigidbody2D>().AddForce(Direction * force);
        Instantiate(bullet, ShootPoint.position, ShootPoint.rotation);

    }
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
