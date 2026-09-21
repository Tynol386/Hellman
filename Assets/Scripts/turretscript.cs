using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class turretscript : MonoBehaviour
{
    [SerializeField]
    Transform player;

    [SerializeField]
    float AgroRange;

    [SerializeField]
    int health = 3;

    Rigidbody2D rb2d;
    Animator animator;

    public GameObject bullet;

    public bool isFacingLeft;

    public float shootDelay = .5f;

    private bool isShooting;

    public float Firerate;

    [SerializeField]
    Transform BulletSpawnPos;
    //materials
    private Material matWhite;
    private Material matDeafult;
    private UnityEngine.Object explosionRef;
    SpriteRenderer sr;

    AudioSource audioSrc;

    float nextTimeToFire = 0;

    SpriteRenderer spriteRenderer;

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
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isFacingLeft == false)
        {
            spriteRenderer.flipX = true;
        }
        //dystans do gracza
        float distToPlayer = Vector2.Distance(transform.position, player.position);

        if (distToPlayer <= AgroRange)
        {
            if (Time.time > nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1 / Firerate;
                shoot();
            }
        }
        else
        {
            //przestañ atakowaæ
            Stopshoot();
        }
    }
    void shoot()
    {
        GameObject b = Instantiate(bullet);
        b.GetComponent<BulletScript>().StartShoot(isFacingLeft);
        b.transform.position = BulletSpawnPos.transform.position;

        Invoke("ResetShoot", shootDelay);
    }
    private void Stopshoot()
    {
        isShooting = false;
    }
    void ResetShoot()
    {
        isShooting = false;
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
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, AgroRange);
    }
}
