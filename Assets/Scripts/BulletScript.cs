using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    [SerializeField]
    float speed;

    [SerializeField]
    int damage;

    [SerializeField]
    float TimeToDestroy = 3;


    public void StartShoot(bool isFacingLeft)
    {
        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
        if (isFacingLeft)
        {
            rb2d.velocity = new Vector2(-speed, 0);
        }
        else
        {
            rb2d.velocity = new Vector2(speed, 0);
        }

        Destroy(gameObject, TimeToDestroy);
       
    }
    /*private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "sciana")
        {
            Destroy(gameObject);
        }
    }*/
}
