using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class finalboss : MonoBehaviour
{
    [SerializeField]
    Transform player;

    [SerializeField]
    float AgroRange;

    [SerializeField]
    float ShootRange;

    [SerializeField]
    float MoveSpeed;

    [SerializeField]
    int health = 40;

    [SerializeField]
    Transform BulletSpawnPos;

    private bool isShooting;

    public float Firerate;

    public float shootDelay = .5f;

    public bool isFacingLeft;

    public GameObject bullet;

    float nextTimeToFire = 0;

    Rigidbody2D rb2d;
    Animator animator;

    public bool isAlive = true;

    public GameObject floor;

    public Slider healthbar;
    public Image zdj;

    //materia³y
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
        healthbar.value = health;
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
        if(distToPlayer <= ShootRange)
        {
            if (Time.time > nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1 / Firerate;
                shoot();
            }
        }
        else
        {
            Stopshoot();
        }
    }
    private void ChasePlayer()
    {
        if (transform.position.x < player.position.x)
        {
            rb2d.velocity = new Vector2(MoveSpeed, 0);
            animator.Play("boss_walk_right");
            isFacingLeft = false;
        }
        else
        {
            rb2d.velocity = new Vector2(-MoveSpeed, 0);
            animator.Play("boss_walk_left");
            isFacingLeft = true;
        }
    }
    void shoot()
    {
        GameObject b = Instantiate(bullet);
        b.GetComponent<BulletScript>().StartShoot(isFacingLeft);
        b.transform.position = BulletSpawnPos.transform.position;

        Invoke("ResetShoot", shootDelay);
    }
    private void StopChPlayer()
    {
        rb2d.velocity = new Vector2(0, 0);
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
        isAlive = false;
        if (isAlive == false)
        {
            Destroy(healthbar.gameObject);
            Destroy(zdj.gameObject);
            Destroy(floor);
        }
        explosion.transform.position = new Vector3(transform.position.x, transform.position.y + .3f, transform.position.z);
        Destroy(gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, AgroRange);
    }
}
