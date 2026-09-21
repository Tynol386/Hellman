using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class credits : MonoBehaviour
{
    IEnumerator waiter()
    {
        yield return new WaitForSeconds(19);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 12);
    }


    // Update is called once per frame
    void Update()
    {
        StartCoroutine(waiter());
    }

}
