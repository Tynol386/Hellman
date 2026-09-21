using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pdeath : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "death")
        {
            transform.position = new Vector2(1, 0);
        }
        if (collision.gameObject.tag == "Enemy")
        {
            transform.position = new Vector2(1, 0);
        }
        if (collision.gameObject.tag == "death2")
        {
            transform.position = new Vector2(92, -11);
        }
        if (collision.gameObject.tag == "Enemy2")
        {
            transform.position = new Vector2(92, -11);
        }
        if (collision.gameObject.tag =="turret")
        {
            transform.position = new Vector2(113, -75);
        }
        if (collision.gameObject.tag =="tp")
        {
            transform.position = new Vector2(44, -94);
        }
        if (collision.gameObject.tag == "boss3")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (collision.gameObject.tag == "boss_turret")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
        
}
