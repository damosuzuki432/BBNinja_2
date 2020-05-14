﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Koban : MonoBehaviour
{
    //SFX reference
    [SerializeField] AudioClip audioClip;
    [SerializeField] public int points = 500;

    //showscore
    [SerializeField] GameObject showScore;
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
            TriggerSFX();
            ShowScore();
            Destroy(gameObject);
            AddScore();

        }
    }
    private void TriggerSFX()
    {
        AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position);
    }

    private void ShowScore()
    {
        GameObject showScoretext = Instantiate(showScore, transform.position, transform.rotation);
        showScoretext.AddComponent<Rigidbody2D>().velocity = new Vector2(0, 0.5f);
        Destroy(showScoretext, 2.0f);
    }

    private void AddScore()
    {
        FindObjectOfType<GameSession>().Score(points);
    }
}
