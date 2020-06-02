using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenItem : MonoBehaviour
{
    [SerializeField] GameObject Shuriken;
    paddle paddle;
    [SerializeField] AudioClip powerUpSound;
    float distance = 0.6f; //y distance btw paddle to instantiate
    // Start is called before the first frame update
    GameSession gameSession;

    void Start()
    {
        Destroy(gameObject, 10.0f); //self destruction in 10 sec. to avoid remainging forever
        gameSession = FindObjectOfType<GameSession>();

    }

    // Update is called once per frame
    void Update()
    {
        if (gameSession == null
            ||gameSession.state == GameSession.State.title
            || gameSession.state == GameSession.State.Special)
        {
            Destroy(gameObject);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Paddle")
        {
            paddle = FindObjectOfType<paddle>();
            Vector2 shurikenPos = new Vector2(paddle.transform.position.x, paddle.transform.position.y + distance);
            Instantiate(Shuriken, shurikenPos, transform.rotation);
            AudioSource.PlayClipAtPoint(powerUpSound,Camera.main.transform.position);
            Destroy(gameObject);
        }
    }
}
