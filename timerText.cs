using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class timerText : MonoBehaviour
{
    [SerializeField] public float seconds = 45.0f;
    private TextMeshProUGUI timerTextUGUI;
  
    Ball ball;
    GameSession gameSession;

    // Start is called before the first frame update
    void Start()
    {
        timerTextUGUI = GetComponentInChildren<TextMeshProUGUI>();
        ball = FindObjectOfType<Ball>();
        gameSession = FindObjectOfType<GameSession>();
        timerTextUGUI.text = seconds.ToString("00");
    }

    // Update is called once per frame
    void Update()
    {
        if(gameSession  == null) { gameSession = FindObjectOfType<GameSession>(); }//avoid reference error
        if(gameSession.state == GameSession.State.Playable)
        {
            if (ball.hasStarted)
            {
                seconds -= Time.deltaTime;
                if (seconds <= 0f) { return; }
                timerTextUGUI.text = seconds.ToString("00");
            }
        }
    }

    
}
