using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAgro2 : MonoBehaviour
{
    [SerializeField]
    Transform player;

    [SerializeField]
    float AgroRange;

    [SerializeField]
    float MoveSpeed;

    [SerializeField]
    int health = 3;

    Rigidbody2D rb2d;
    Animator animator;

    //materials
    private Material matWhite;
    private Material matDeafult;
    private UnityEngine.Object explosionRef;
    SpriteRenderer sr;

    AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        matWhite = Resources.Load("WhiteFlash", typeof(Material)) as Material;
        matDeafult = sr.material;
        explosionRef = Resources.Load("Explosion");
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //dystans do gracza
        float distToPlayer = Vector2.Distance(transform.position, player.position);

        if (distToPlayer <= AgroRange)
        {
            //skrypt atakuj¹cy gracza
            ChasePlayer();
        }
        else
        {
            //przestañ atakowaæ
            StopChPlayer();
            animator.Play("enemy2_idle");
        }
    }
    private void ChasePlayer()
    {
        if (transform.position.x < player.position.x)
        {
            rb2d.velocity = new Vector2(MoveSpeed, 0);
            animator.Play("enemy2_run2");
        }
        else
        {
            rb2d.velocity = new Vector2(-MoveSpeed, 0);
            animator.Play("enemy2_run");
        }
    }
    private void StopChPlayer()
    {
        rb2d.velocity = new Vector2(0, 0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            health--;
            sr.material = matWhite;
            audioSrc.Play();
            if (health <= 0)
            {
                KillSelf();
            }
            else
            {
                Invoke("ResetMaterial", .1f);
            }


        }
    }
    void ResetMaterial()
    {
        sr.material = matDeafult;
    }
    void KillSelf()
    {
        GameObject explosion = (GameObject)Instantiate(explosionRef);
        explosion.transform.position = new Vector3(transform.position.x, transform.position.y + .3f, transform.position.z);
        Destroy(gameObject);
    }

}
