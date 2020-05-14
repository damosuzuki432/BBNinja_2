using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier2Itself : MonoBehaviour
{

    /// <summary>
    /// attached to barrier 2 (orange) itself. 
    /// </summary>

    [SerializeField] AudioClip barrierSFX;
    int hitPoint = 0;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            hitPoint++;
            AudioSource.PlayClipAtPoint(barrierSFX, Camera.main.transform.position);
            if (hitPoint > 1)
            {
                FindObjectOfType<Barrier2>().barrier2On = false;
                Destroy(gameObject);
            }
        }
    }

    public void SelfDestruct()  //TODO what was this...?
    {
        Destroy(gameObject);
        FindObjectOfType<Barrier2>().barrier2On = false;
    }


}
