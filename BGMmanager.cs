using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMmanager : MonoBehaviour
{
    /// <summary>
    /// manage the bgm of the entire scene, other than opening scene.
    /// </summary>


    string StageName; //to see the stage num
    public AudioSource BGM_Stage1;
    public AudioSource BGM_Stage2;
    public AudioSource BGM_Stage3;
    public AudioSource BGM_Stage4;
    public AudioSource BGM_Stage5;
    public AudioSource BGM_GameOver;

    private string previousScene = "Stage1-9"; //the name of the previous scene

    private void Awake()
    {
        int bgmManagerCount = FindObjectsOfType<BGMmanager>().Length;
        if (bgmManagerCount > 1 )
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
        StageName = SceneManager.GetActiveScene().name;
        string chkStageName = StageName.Substring(5, 1); //see the 6th alphabet of the stagename

        //BGM_stage1 is play on awake, so it has to stop when scene is other than stage1-x.

        if (chkStageName == "2")
        {
            BGM_Stage1.Stop();
            BGM_Stage2.Play();
        }
        else if (chkStageName == "3")
        {
            BGM_Stage1.Stop();
            BGM_Stage3.Play();
        }
        else if (chkStageName == "4")
        {
            BGM_Stage1.Stop();
            BGM_Stage4.Play();
        }
        else if (chkStageName == "5")
        {
            BGM_Stage1.Stop();
            BGM_Stage5.Play();
        }

    }

    void LoadNextScene()
    {
        Debug.LogError("what's the purpose of this?");
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
    {
        Debug.Log("error1");
        if (gameObject != null) 
        {
            if (nextScene.name == "Stage1-1")
            {
                if (BGM_Stage1.isPlaying) { return; }
                BGM_GameOver.Stop();
                BGM_Stage1.Play();
            }
            else if (nextScene.name == "Stage2-1")
            {
                if (BGM_Stage2.isPlaying) { return; }
                BGM_GameOver.Stop();
                BGM_Stage1.Stop();
                BGM_Stage2.Play();
            }
            else if (nextScene.name == "Stage3-1")
            {
                if (BGM_Stage3.isPlaying) { return; }
                BGM_GameOver.Stop();
                BGM_Stage2.Stop();
                BGM_Stage3.Play();
            }
            else if (nextScene.name == "Stage4-1")
            {
                if (BGM_Stage4.isPlaying) { return; }
                BGM_GameOver.Stop();
                BGM_Stage3.Stop();
                BGM_Stage4.Play();
            }
            else if (nextScene.name == "Stage5-1")
            {
                if (BGM_Stage5.isPlaying) { return; }
                BGM_Stage4.Stop();
                BGM_Stage5.Play();
            }
            else if (nextScene.name == "GameOver")
            {
                BGM_Stage1.Stop();
                BGM_Stage2.Stop();
                BGM_Stage3.Stop();
                BGM_Stage4.Stop();
                BGM_GameOver.Play();
            }
            else if (nextScene.name == "NanoLogo")
            {
                Destroy(gameObject);
            }
        }

      
    }
   
}
