using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;


public class GameSession : MonoBehaviour
{
/*
This is attached to "GameSession".
the main funtions are:
 1. control game speed. you can either increase/decrease the entire speed of the game.
 2. manage scoreborad. It is called by block class because these two are closely related.
 3. TODO manage BGM of the game.
 4. 1,2,3 must be maintained through the entire gamesession, so the concept of Singlton comes in.
    that is why you see Dont Destroy on Load below.
    if you need one thing, such as score, music, etc, and want to use it throuhg entire game,
    lets get it together in ONE class like below.   
 */

    //Game speed params
    [Range(0f,10f)][SerializeField] float timeScale = 1.0f;

    //Game score params
    public int score = 0;
    [SerializeField] TextMeshProUGUI scoreNum;
    [SerializeField] GameObject Curtain; //used to hide gameinfo when gameover scene
    string StageName; //to see the stage num
    //Game Debug bool
    [SerializeField] bool isAutoPlayEnabled;
    public AudioSource[] audioClip;

    bool scoreThre1 = false;
    bool scoreThre2 = false;
    bool scoreThre3 = false;
    bool scoreThre4 = false;
    int threshold = 20000;

    paddle paddle;

    public enum State { title, Playable, Special, Clear } // Clear is unused.
    public State state;

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.activeSceneChanged += OnActiveSceneChanged;
    }

    void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
    {
          if (gameObject != null)
            {
                string sceneName = SceneManager.GetActiveScene().name;
                Curtain.SetActive(false);

                if (sceneName == "GameOver")
                {
                    Curtain.SetActive(true);
                }
                else if (sceneName == "NanoLogo")
                {
                    Destroy(gameObject);
                }
            }
    }


    private void Awake()
    {
       
        int gameSessionCount = FindObjectsOfType<GameSession>().Length;
        if (gameSessionCount > 1) // if one already exists...
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Manage Speed of the Game
        Time.timeScale = timeScale;
        //Debug.Log(score);
        
    }

    

    public void Score(int scorePerBreak)
    {
        //get accumurated score
        score += scorePerBreak;
        
        //display score on scoreNumtext;
        scoreNum.text = score.ToString();
        ScoreThreshold();

    }

    public void ResetScore()
    {
        score = 0;
        scoreNum.text = score.ToString();
    }

    private void ScoreThreshold()
    {
        if(score > threshold)
        {
                FindObjectOfType<LifePanel>().IncreaseLife();       
                threshold += threshold;
        }       
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }

    public bool IsAutoPlayEnabled()
    {
        return isAutoPlayEnabled;
    }

    
    //TODO　シーンの切り替え時　title
    //TODO ２秒後　playable
    public void ToTitleState()
    {
        state = GameSession.State.title;
    }
    public void ToPlayableState()
    {
        state = GameSession.State.Playable;
    }



}
