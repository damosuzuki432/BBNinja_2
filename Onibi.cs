using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Onibi : MonoBehaviour
{
    [SerializeField] AudioClip hitTrap;
    [SerializeField] AudioClip onibiAppear;


    GameSession gameSession;
    Rigidbody2D rb2D;
    LevelManager levelManager;
    Ball ball;

    private void Start()
    {
        Destroy(gameObject, 10.0f); //self destruction in 10 sec. to avoid remainging forever
        gameSession = FindObjectOfType<GameSession>();
        ball = FindObjectOfType<Ball>();
        rb2D = GetComponent<Rigidbody2D>();
        levelManager = FindObjectOfType<LevelManager>();
        AudioSource.PlayClipAtPoint(onibiAppear, Camera.main.transform.position);


    }

    private void Update()
    {
    
        if (gameSession.state == GameSession.State.title
            || gameSession.state == GameSession.State.Special)
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Paddle")
        {
            StartCoroutine(panic());
            Destroy(gameObject,3.0f);
        }
    }

    IEnumerator panic()
    {
        FindObjectOfType<paddle>().GetComponent<Animator>().SetBool("panic", true);
        AudioSource.PlayClipAtPoint(hitTrap, Camera.main.transform.position);
        yield return new WaitForSeconds(1.0f);
        FindObjectOfType<paddle>().GetComponent<Animator>().SetBool("panic", false);
        yield break;
    }
}
