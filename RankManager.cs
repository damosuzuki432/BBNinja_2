using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RankManager : MonoBehaviour
{
    /// <summary>
    /// attached only to stage 1-1 cause ranking needs to be decided from the beginning
    /// </summary>

    public float wholeSessionTime = 13000; //time used to rank player at the end of the game
    GameSession gameSession;
    Ball ball;

    private void Awake()
    {

        int rankManagerCount = FindObjectsOfType<RankManager>().Length;
        if (rankManagerCount > 1) // if one already exists...
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
        SceneManager.activeSceneChanged += OnActiveSceneChanged;
        gameSession = FindObjectOfType<GameSession>();
        ball = FindObjectOfType<Ball>();
    }

    void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
    {
        if (gameObject != null)
        {
            string sceneName = SceneManager.GetActiveScene().name;
            if (sceneName == "NanoLogo")
            {
                Destroy(gameObject);
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "Stage5-1"|| sceneName != "Stage4-1") //do not count time in Boss stage and finale
        {
            return;
        }
        else
        {
            TimeCounter();
        }
        
    }

    private void TimeCounter()
    {
        if (gameSession == null) { gameSession = FindObjectOfType<GameSession>(); }//avoid reference error
        if (gameSession.state == GameSession.State.Playable)
        {
            if (ball == null) { ball = FindObjectOfType<Ball>(); }
            if (ball.hasStarted)
            {
                wholeSessionTime += Time.deltaTime;
            }
        }
    }
}
