using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Addlife : MonoBehaviour
{
    /// <summary>
    /// attached to AddlifeItem(small ninja) to increase life when collided.
    /// destroy self in 10 sec.
    /// </summary>


    LifePanel lifePanel;
    paddle paddle;
    GameSession gameSession;

    private void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        Destroy(gameObject, 10.0f); //self destruction in 10 sec. to avoid remainging forever
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Paddle")
        {
            lifePanel = FindObjectOfType<LifePanel>();
            lifePanel.IncreaseLife();
            Destroy(gameObject);
        }

    }
    private void Update()
    {
        if (gameSession.state == GameSession.State.title
            || gameSession.state == GameSession.State.Special)
        {
            Destroy(gameObject);
        }

    }
}
