using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireItem : MonoBehaviour
{
    [SerializeField] GameObject Fire;
    paddle paddle;
    [SerializeField] AudioClip powerUpSound;
    GameSession gameSession;

    private void Start()
    {
        Destroy(gameObject, 10.0f); //self destruction in 10 sec. to avoid remainging forever
        gameSession = FindObjectOfType<GameSession>();

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
            AudioSource.PlayClipAtPoint(powerUpSound, Camera.main.transform.position);
            Destroy(gameObject);
            StartCoroutine(CreateFire());

        }
    }

    IEnumerator CreateFire()
    {
        int randomY = Random.Range(5, 8);
        Vector2 firePos = new Vector2(11.0f, randomY);
        Quaternion rote = Quaternion.Euler(0.0f, -90.0f, 90.0f);
        GameObject FireVFX = Instantiate(Fire, firePos, rote);
        Destroy(FireVFX, 2.5f);
        yield break;
    }

    
}
