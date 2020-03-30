using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    [SerializeField] AudioClip barrierSFX;

    private void Awake()
    {
        int barrierCount = FindObjectsOfType<Barrier>().Length;
        if (barrierCount > 1)
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
            gameObject.SetActive(false);
        }
    }

}
