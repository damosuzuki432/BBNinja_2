using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierItself : MonoBehaviour
{
    [SerializeField] AudioClip barrierSFX;
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            FindObjectOfType<Barrier>().barrierOn = false;
            AudioSource.PlayClipAtPoint(barrierSFX, Camera.main.transform.position);
            Destroy(gameObject);          
        }
    }

    public void SelfDestruct()
    {
        Destroy(gameObject);
        FindObjectOfType<Barrier>().barrierOn = false;
    }

}
