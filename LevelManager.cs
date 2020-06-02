using System;
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
    public bool multiClear = false;  //refered by Rareitem

    public int reqdNumBlock;
    [SerializeField] TextMeshProUGUI reqdBlocks;
    [SerializeField] TextMeshProUGUI okText;
    [SerializeField] GameObject clearImage;
    [SerializeField] GameObject normalClearImage;
    [SerializeField] GameObject stageReadyPanel;
    [SerializeField] GameObject timeUpImage;
    [SerializeField] AudioSource backGroundMusic; // destroy when cleared
    [SerializeField] AudioClip clearJingle; // to play when cleared
    [SerializeField] AudioClip reqZeroJingle; // to play when reqd = zero
    [SerializeField]float reqdRatio = 0.8f;

    bool reqZero = false;
    string stgName;

    AudioSource audioSource;


    Ball ball;
    timerText timerText;
    GameSession gameSession;
    
    private void Start()
    {
        ball = FindObjectOfType<Ball>();
        timerText = FindObjectOfType<timerText>();
        gameSession = FindObjectOfType<GameSession>();
        stgName = SceneManager.GetActiveScene().name;
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
        if (stgName != "Stage4-1") { LoadingManager(); }
    }

    private void LoadingManager() 
    {
        numblockObject = numblockObjects.Length;
        if(reqdNumBlock == 0)
        {
            if(reqZero == false) //show OK sign
            {
                if (reqZeroJingle == null) { return; }
                else
                {
                    AudioSource.PlayClipAtPoint(reqZeroJingle, Camera.main.transform.position);
                    reqdBlocks.enabled = false;
                    okText.gameObject.SetActive(true);
                    reqZero = true;
                }
            }
        }

        if (numblockObject == 0) 
        {
            if (multiClear == true) {return;}
           
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

    public void StartDeathSequence()
    {
        timeUpImage.SetActive(true);
        ball.gameObject.SetActive(false); //inactivate ball
        FindObjectOfType<LifePanel>().DecraeseLife();
        FindObjectOfType<GameSession>().ToTitleState();
        FindObjectOfType<ChargeManager>().ResetCharge();
        multiDeath = true;

    }

    public void invokeClearSeq()//called by rareitem
    {
        Invoke("StartClearSequence", 3.0f);
    }

    private void StartClearSequence() 
    {

        FindObjectOfType<GameSession>().ToTitleState();
        ball.gameObject.SetActive(false); //inactivate ball
        multiClear = true; //prevent multiple clear seq

       

        //show clear image depending on scene number 
        string StageName = SceneManager.GetActiveScene().name;
        if(StageName == "Stage4-1")
        {
            //GameClear();
        }

        else if (StageName == "Stage1-9" || StageName == "Stage2-9"|| StageName == "Stage3-9")
        {
            ClearSection();
        }

        else if (numblockObject == 0)
        {
            clearImage.SetActive(true); //show perfect clear image
            Invoke("LoadNextScene", 2.0f);

        }
        else if (numblockObject != 0)
        {
            normalClearImage.SetActive(true);
            Invoke("LoadNextScene", 2.0f);

        }

    }

    private void ClearSection()
    {
        FindObjectOfType<BGMmanager>().BGM_Stage1.Stop();
        FindObjectOfType<BGMmanager>().BGM_Stage2.Stop();
        FindObjectOfType<BGMmanager>().BGM_Stage3.Stop();
        AudioSource.PlayClipAtPoint(clearJingle, Camera.main.transform.position); //play clear jingle
        clearImage.SetActive(true);
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
