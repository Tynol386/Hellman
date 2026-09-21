using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Playerm : MonoBehaviour
{

    AudioSource audioSrc;
    Animator animator;
    Rigidbody2D rb2d;
    SpriteRenderer spriteRenderer;
    public bool canJump = true;
    private bool isShooting;

    [SerializeField]
    GameObject bullet;

    [SerializeField]
    Transform BulletSpawnPos;

    [SerializeField]
    private float shootDelay = .5f;

    [SerializeField]
    private AudioSource Jump;

    bool isFacingLeft;

    [SerializeField]
    private AudioSource coin_sound;

    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (isShooting) return;
            //SHOOT
            isShooting = true;

            audioSrc.Play();

            GameObject b = Instantiate(bullet);
            b.GetComponent<BulletScript>().StartShoot(isFacingLeft);
            b.transform.position = BulletSpawnPos.transform.position;

            Invoke("ResetShoot", shootDelay);
        }
    }

    void ResetShoot()
    {
        isShooting = false;
    }

    private void FixedUpdate()
    {
        if(Input.GetKey("d") || Input.GetKey("right"))
        {
            rb2d.velocity = new Vector2(7, rb2d.velocity.y);

                    if(canJump==true) 
                        animator.Play("Player_run");

            spriteRenderer.flipX = false;
            isFacingLeft = false;
        }
        else if (Input.GetKey("a") || Input.GetKey("left"))
        {
            rb2d.velocity = new Vector2(-7, rb2d.velocity.y);

            if (canJump == true)
                animator.Play("Player_run");

            spriteRenderer.flipX = true;
            isFacingLeft = true;
        }
        else
        {
            if (canJump == true)
                animator.Play("Player_idle");
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        }
        if (canJump == true && Input.GetKey("space") || canJump == true && Input.GetKey("up"))
        {
            Jump.Play();
            rb2d.velocity = new Vector2(rb2d.velocity.x, 10);
            animator.Play("Player_jump");
        }
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag=="floor")
        {
            canJump = true;
        }
        if(other.gameObject.tag=="exit1")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if (other.gameObject.tag == "coin")
        {
            coin_sound.Play();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "floor")
        {
            canJump = false;
        }
    }
}
