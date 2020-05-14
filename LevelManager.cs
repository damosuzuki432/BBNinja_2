﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
/*
This is attached to "LevelManager".
the main funtions are:
 1. check the number of existing blocks by Tag.
 2. Load next scene when 1. is equal to zero.
*/

    //GameObject reference tagged "block"
    GameObject[] numblockObjects;

    //Return the length of Gameobject tagged "block"
    public int numblockObject;

    //use to avoid mutiple death at Update method
    bool multiDeath = false;

    //use to avoid multiple clear at Update method
    bool multiClear = false;

    public int reqdNumBlock;
    [SerializeField] TextMeshProUGUI reqdBlocks;
    [SerializeField] GameObject clearImage;
    [SerializeField] GameObject stageReadyPanel;
    [SerializeField] AudioSource backGroundMusic; // destroy when cleared
    [SerializeField] AudioClip clearJingle; // to play when cleared
    [SerializeField]float reqdRatio = 0.8f;

    AudioSource audioSource;

    Ball ball;
    timerText timerText;
    GameSession gameSession;
    
    private void Start()
    {
        ball = FindObjectOfType<Ball>();
        timerText = FindObjectOfType<timerText>();
        gameSession = FindObjectOfType<GameSession>();
        CheckNumOfBlocks("Block"); //TODO NOTE that the tag is hard coded
        ShowRequiredBlocks();
        Invoke("StageReady", 2.0f);
       
    }

  

    private void StageReady()
    {
        stageReadyPanel.SetActive(false);
        FindObjectOfType<GameSession>().ToPlayableState();
    }

    public void CheckNumOfBlocks(string blockTagObjects)
    {
        numblockObjects = GameObject.FindGameObjectsWithTag(blockTagObjects);
    }
    private void ShowRequiredBlocks()
    {
        numblockObject = numblockObjects.Length;
        reqdNumBlock = Mathf.FloorToInt(numblockObject * reqdRatio);
        reqdBlocks.text = reqdNumBlock.ToString();
    }

    
    // Update is called once per frame
    void Update()
    {
        CheckNumOfBlocks("Block");
        LoadingManager();
    }

    private void LoadingManager() 
    {
        numblockObject = numblockObjects.Length;
        if (numblockObject == 0)
        {
            if (multiClear == true) {return;}
            if (gameSession.state == GameSession.State.Special)//if state == special, wait for a while till the VFX finsh to start StartClearSequence
            {
                ball.gameObject.SetActive(false); //inactivate ball
                Invoke("StartClearSequence", 3.0f);
            }
            else
            {
                StartClearSequence();
            }
        }

        if (reqdNumBlock == 0　&& timerText.seconds < 0)
        {
            if (multiClear == true) {return;}
            StartClearSequence();
        }
        if (reqdNumBlock >=1 && timerText.seconds < 0)
        {
            if (multiDeath == true) {return;}
            else
            {
                StartDeathSequence();
            }
        }
    }

    private void StartDeathSequence()
    {
        ball.gameObject.SetActive(false); //inactivate ball
        FindObjectOfType<LifePanel>().DecraeseLife();
        FindObjectOfType<GameSession>().ToTitleState();
        FindObjectOfType<ChargeManager>().ResetCharge();
        multiDeath = true;
        //gameSession.state = GameSession.State.title;
    }

    private void StartClearSequence()
    {

        FindObjectOfType<GameSession>().ToTitleState();
        ball.gameObject.SetActive(false); //inactivate ball
        clearImage.SetActive(true); //show clear image
        multiClear = true; //prevent multiple clear seq
        string StageName = SceneManager.GetActiveScene().name;
        if(StageName == "Stage4-1")
        {

            GameClear();
        }
        else if (StageName == "Stage1-9" || StageName == "Stage2-9"|| StageName == "Stage3-9")
        {
            ClearSection();
        }
        //TODO stop time
        //TODO show button to next stage
        //TODO below is temporary
        else
        {

            Invoke("LoadNextScene", 2.0f);
        }
    }

    private void GameClear()
    {

        gameSession = FindObjectOfType<GameSession>();
        gameSession.gameObject.SetActive(false);

        
        StartCoroutine(ToEnding());
        

    }

    IEnumerator ToEnding()
    {
        FindObjectOfType<BGMmanager>().BGM_Stage4.Stop();
       
        yield return new WaitForSeconds(7f);
       
        FindObjectOfType<BGMmanager>().BGM_Stage5.Play();
        yield return new WaitForSeconds(1.5f);
        FindObjectOfType<SceneLoader>().LoadNextScene();
        yield break;
    }

    private void ClearSection()
    {
        FindObjectOfType<BGMmanager>().BGM_Stage1.Stop();
        FindObjectOfType<BGMmanager>().BGM_Stage2.Stop();
        FindObjectOfType<BGMmanager>().BGM_Stage3.Stop();
        AudioSource.PlayClipAtPoint(clearJingle, Camera.main.transform.position); //play clear jingle
        Invoke("LoadNextScene", 4.0f);
    }

    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
        
    }

    public void DecreaseBlock()
    {
        if (reqdNumBlock > 0) //prevent reqdNumBlock from being less than 0
        {
            reqdNumBlock--;
            reqdBlocks.text = reqdNumBlock.ToString();
        }
        numblockObject--;
        Debug.Log(numblockObject);
    }
}
