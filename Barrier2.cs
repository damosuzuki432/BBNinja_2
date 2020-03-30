using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier2 : MonoBehaviour
{
    [SerializeField] AudioClip barrierSFX;
    int HitPoint = 1;

    private void Awake()
    {
        int barrier2Count = FindObjectsOfType<Barrier2>().Length;
        if (barrier2Count > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            AudioSource.PlayClipAtPoint(barrierSFX, Camera.main.transform.position);
            HitPoint++;
            if(HitPoint>2)
            {
                gameObject.SetActive(false);
            }
        }
    }

}
